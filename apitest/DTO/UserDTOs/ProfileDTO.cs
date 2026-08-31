using System.Text.Json.Serialization;

namespace apitest.DTO.UserDTOs;

public record ProfileDTO
{
    [JsonPropertyName("fullName")]
    public string FullName { get; init; }

    [JsonPropertyName("age")]
    public int Age { get; init; }

    [JsonPropertyName("address")]
    public AddressDTO Address { get; init; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; init; }
}