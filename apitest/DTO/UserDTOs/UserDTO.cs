using System.Text.Json.Serialization;

namespace apitest.DTO.UserDTOs;

public record UserDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("profile")]
    public ProfileDTO Profile { get; set; }

    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; }
}