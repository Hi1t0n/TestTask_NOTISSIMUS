using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Domain.Interfaces;
using Server.Infrastructure.Context;
using Server.Infrastructure.Repository;

namespace Server.Infrastructure.Extensions;

/// <summary>
/// Класс методов расширения для <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление бизнес логики
    /// </summary>
    /// <param name="serviceCollection"><see cref="IServiceCollection"/>></param>
    /// <param name="connectionString">Строка подключения</param>
    /// <returns>Модифицированный <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDatabase(connectionString);
        serviceCollection.AddServices();
        return serviceCollection;
    }

    /// <summary>
    /// Добавление сервисов
    /// </summary>
    /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
    /// <returns>Модифицированный <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        return serviceCollection;
    }
    
    /// <summary>
    /// обавление базы данных
    /// </summary>
    /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
    /// <param name="connectionString">Строка подключения</param>
    /// <returns>Модифицированный <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(x=> x.UseSqlite(connectionString));
        return serviceCollection;
    }
}