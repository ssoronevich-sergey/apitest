using apitest.Interfaces.DapperInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using FluentAssertions;

namespace apitest;

public class DapperTest
{
    private readonly TestPrecondition _precondition = new();
    private SqliteConnection? _connection;

    [SetUp]
    public async Task Setup()
    {
        // Используем In-Memory БД
        _connection = new SqliteConnection("Data Source=:memory:");
        await _connection.OpenAsync();
        await DatabaseInitializer.InitializeAsync(_connection);
    }

    [TearDown]
    public async Task TearDown()
    {
        // ✅ Освобождаем ресурсы ПОСЛЕ каждого теста
        if (_connection != null)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }
    }

    [Test]
    public async Task GetAllUsers()
    {
        var repo = _precondition.Provider.GetRequiredService<IUserRepository>();
        var users = await repo.GetAllAsync();
        users.Should().HaveCount(15);
    }
}