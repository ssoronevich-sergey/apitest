using System.Text.Json.Serialization;

namespace apitest.DTO.UserDTOs;

public record RootDTO
{
    [JsonPropertyName("data")]
    public List<UserDTO> Data { get; init; }
}
