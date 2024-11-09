namespace Server.Domain.Contracts;

/// <summary>
/// DTO для добавления пользователя
/// </summary>
/// <param name="Login">Логин</param>
public record AddUserRequest(string Login);