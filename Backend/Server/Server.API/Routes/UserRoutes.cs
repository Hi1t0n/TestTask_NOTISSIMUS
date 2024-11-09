using Microsoft.AspNetCore.Mvc;
using Server.Domain.Contracts;
using Server.Domain.Interfaces;
using Server.Domain.Models;

namespace Server.API.Routes;

public static class UserRoutes
{
    public static WebApplication AddUserRoutes(this WebApplication application)
    {
        var userGroup = application.MapGroup("/api/users");

        userGroup.MapPost(pattern: "/", handler: AddUser);
        userGroup.MapGet(pattern: "/{partlogin}", handler: GetUserByPartLogin);
        
        return application;
    }

    public static async Task<IResult> AddUser(AddUserRequest request, IUserRepository userRepository)
    {
        if (string.IsNullOrWhiteSpace(request.Login))
        {
            return Results.BadRequest("Введите логин");
        }
        
        var result = await userRepository.AddUserAsync(request.Login.ToLower());

        if (result is null)
        {
            return Results.BadRequest($"Логин: {request.Login} же занят");
        }

        return Results.Ok();
    }

    public static async Task<IResult> GetUserByPartLogin(string partLogin,
        IUserRepository userRepository)
    {
        var users = await userRepository.GetUsersByPartLoginAsync(partLogin.ToLower());
        
        if (users.Count == 0)
        {
            return Results.NotFound($"Пользователя подходящего под часть логина '{partLogin}' не найдено");
        }
        
        return Results.Ok(users);
    }
    
}