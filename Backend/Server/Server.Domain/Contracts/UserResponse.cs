namespace Server.Domain.Contracts;

/// <summary>
/// DTO для воврата данных пользователя на клиента
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Login">Логин</param>
public record UserResponse(Guid Id, string Login);