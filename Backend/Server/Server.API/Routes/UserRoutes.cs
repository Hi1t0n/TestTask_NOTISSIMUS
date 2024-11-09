using Microsoft.AspNetCore.Mvc;
using Server.Domain.Contracts;
using Server.Domain.Interfaces;
using Server.Domain.Models;

namespace Server.API.Routes;

/// <summary>
/// Руты для полььзователей
/// </summary>
public static class UserRoutes
{
    /// <summary>
    /// Добавление рутов для пользователей
    /// </summary>
    /// <param name="application"><see cref="WebApplication"/>></param>
    /// <returns><see cref="WebApplication"/></returns>
    public static WebApplication AddUserRoutes(this WebApplication application)
    {
        var userGroup = application.MapGroup("/api/users");

        userGroup.MapPost(pattern: "/", handler: AddUser);
        userGroup.MapGet(pattern: "/{partlogin}", handler: GetUserByPartLogin);
        
        return application;
    }
    
    /// <summary>
    /// Добавление пользователя
    /// </summary>
    /// <param name="request">DTO с данными пользователя для добавления <see cref="AddUserRequest"/>></param>
    /// <param name="userRepository"><see cref="IUserRepository"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="IResult"/></returns>
    public static async Task<IResult> AddUser(AddUserRequest request, IUserRepository userRepository, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Login))
        {
            return Results.BadRequest("Введите логин");
        }
        
        var result = await userRepository.AddUserAsync(request.Login.ToLower(), cancellationToken);

        if (result is null)
        {
            return Results.BadRequest($"Логин: {request.Login} же занят");
        }

        return Results.Ok();
    }

    /// <summary>
    /// Получение пользователей по части логина
    /// </summary>
    /// <param name="partLogin">Часть логина</param>
    /// <param name="userRepository"><see cref="IUserRepository"/>></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="IResult"/>></returns>
    public static async Task<IResult> GetUserByPartLogin(string partLogin,
        IUserRepository userRepository, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetUsersByPartLoginAsync(partLogin.ToLower(), cancellationToken);
        
        if (users.Count == 0)
        {
            return Results.NotFound($"Пользователя подходящего под часть логина '{partLogin}' не найдено");
        }
        
        return Results.Ok(users);
    }
    
}