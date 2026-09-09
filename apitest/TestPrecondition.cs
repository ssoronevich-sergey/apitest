using System.Data;
using apitest.DapperRepository;
using apitest.Interfaces.DapperInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;

namespace apitest;

public class TestPrecondition : IDisposable
{
    public ServiceProvider Provider { get; }
    private readonly SqliteConnection _connection;
    private readonly string _connectionString;
    private bool _disposed = false;

    public TestPrecondition()
    {
        _connectionString = "Data Source=:memory:";
        
        // Создаем и инициализируем соединение
        _connection = new SqliteConnection(_connectionString);
        _connection.Open();
        DatabaseInitializer.InitializeAsync(_connection).GetAwaiter().GetResult();
        
        var services = new ServiceCollection();
        
        // ✅ Регистрируем connection string
        services.AddSingleton(_connectionString);
        
        // ✅ Регистрируем соединение
        services.AddSingleton<IDbConnection>(_connection);
        
        // Регистрируем репозитории
        services.AddScoped<IUserRepository, UserRepository>();
        
        Provider = services.BuildServiceProvider();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _connection?.Dispose();
            Provider?.Dispose();
            _disposed = true;
        }
    }
}