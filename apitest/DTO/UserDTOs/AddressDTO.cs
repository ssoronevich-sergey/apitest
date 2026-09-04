using System.Text.Json.Serialization;

namespace apitest.DTO.UserDTOs;

public record AddressDTO
{
    [JsonPropertyName("street")]
    public string Street { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("geo")]
    public GeoDTO Geo { get; set; }
}