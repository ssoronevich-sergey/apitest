using apitest.DTO.DapperDTOs;
using Microsoft.ApplicationInsights;

namespace apitest.Interfaces.DapperInterfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserDTO>> GetAllAsync();
    
}