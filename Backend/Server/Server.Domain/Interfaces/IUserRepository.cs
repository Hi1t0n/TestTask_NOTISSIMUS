using Server.Domain.Contracts;
using Server.Domain.Models;

namespace Server.Domain.Interfaces;

/// <summary>
/// Интерфейс UserRepository
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Получение пользователей по части логина
    /// </summary>
    /// <param name="loginPart">Часть логина пользователя</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Список пользователей | null</returns>
    Task<List<UserResponse>?> GetUsersByPartLoginAsync(string loginPart,CancellationToken cancellationToken);
    /// <summary>
    /// Добавление пользователя
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Данные пользователя | null</returns>
    Task<User?> AddUserAsync(string login, CancellationToken cancellationToken);
}