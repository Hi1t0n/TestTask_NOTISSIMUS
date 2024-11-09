using Microsoft.EntityFrameworkCore;
using Server.Domain.Contracts;
using Server.Domain.Interfaces;
using Server.Domain.Models;
using Server.Infrastructure.Context;

namespace Server.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<UserResponse>?> GetUsersByPartLoginAsync(string loginPart)
    {
        if (string.IsNullOrWhiteSpace(loginPart))
        {
            return null;
        }
        
        var users = await _context.Users
            .Where(x=> x.Login.Contains(loginPart))
            .Select(x=> new UserResponse(x.Id, x.Login))
            .ToListAsync();
        return users;
    }

    public async Task<User?> AddUserAsync(string login)
    {
        if (await _context.Users.AnyAsync(x=> x.Login == login))
        {
            return null;
        }
        
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Login = login
        };

        await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }
}