using apitest.DapperRepository;
using apitest.Interfaces.DapperInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace apitest;

public static class DataAccessModule
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUserRepository>(p => new UserRepository(connectionString));
        return services;
    }
}