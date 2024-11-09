using Microsoft.EntityFrameworkCore;
using Server.Domain.Contracts;
using Server.Domain.Interfaces;
using Server.Domain.Models;
using Server.Infrastructure.Context;

namespace Server.Infrastructure.Repository;

/// <summary>
/// Реализация интерфейск <see cref="IUserRepository"/>
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    ///<inheritdoc />
    public async Task<List<UserResponse>?> GetUsersByPartLoginAsync(string loginPart, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(loginPart))
        {
            return null;
        }
        
        var users = await _context.Users
            .Where(x=> x.Login.Contains(loginPart))
            .Select(x=> new UserResponse(x.Id, x.Login))
            .ToListAsync(cancellationToken);
        return users;
    }
    
    ///<inheritdoc />
    public async Task<User?> AddUserAsync(string login, CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(x=> x.Login == login, cancellationToken))
        {
            return null;
        }
        
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Login = login
        };

        await _context.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }
}