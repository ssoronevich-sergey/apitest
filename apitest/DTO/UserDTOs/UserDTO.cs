using System.Text.Json.Serialization;

namespace apitest.DTO.UserDTOs;

public record UserDTO
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("username")]
    public string Username { get; init; }

    [JsonPropertyName("profile")]
    public ProfileDTO Profile { get; init; }

    [JsonPropertyName("roles")]
    public List<string> Roles { get; init; }
}