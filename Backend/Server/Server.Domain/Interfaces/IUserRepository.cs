using Server.Domain.Contracts;
using Server.Domain.Models;

namespace Server.Domain.Interfaces;

public interface IUserRepository
{
    Task<List<UserResponse>?> GetUsersByPartLoginAsync(string loginPart);
    Task<User?> AddUserAsync(string login);
}