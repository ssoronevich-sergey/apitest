using apitest.DTO;
using Refit;
namespace apitest.Interfaces;


[Headers("x-api-key:free_user_3Ih9iVe7yOM8DeQVEi3asnhbO3L")]
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