using apitest.DTO.DapperDTOs;
using apitest.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class UserRepository : IUserRepository
{
    private readonly string connectionString;
    public UserRepository(string connection)
    {
        connection = this.connectionString;
    }
    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var users = await db.QueryAsync<UserDTO>("SELECT * FROM Users");
        return users;
    }
}