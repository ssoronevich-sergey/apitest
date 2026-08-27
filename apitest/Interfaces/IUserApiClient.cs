using apitest.DTO;
using Refit;
namespace apitest.Interfaces;


[Headers("x-api-key:free_user_3HpELMQtbGQTO17ItccFNl2nZss")]
public interface IUserApiClient
{
    [Get("/users/{id}")]
    Task<UserResponceDTO> getUserAsync(int id);
    
    [Post("/users")] 
    Task <CreatedUserDTO> PostUserAsync ([Body] CreatedUserDTO user);
    
    [Put("/users/{id}")]
    Task<CreateUserRequestDTO> PutUserAsync(int id, [Body] CreateUserRequestDTO user);
    
    [Delete("/users/{id}")]
    Task<ApiResponse<string>> DeleteUserAsync(int id);
}