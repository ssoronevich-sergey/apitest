using System.Text.Json.Serialization;

namespace apitest.DTO.UserDTOs;

public record AddressDTO
{
    [JsonPropertyName("street")]
    public string Street { get; init; }

    [JsonPropertyName("city")]
    public string City { get; init; }

    [JsonPropertyName("geo")]
    public GeoDTO Geo { get; init; }
}