using System.Text.Json.Serialization;

namespace apitest.DTO.UserDTOs;

public record GeoDTO
{
    [JsonPropertyName("lat")]
    public double Lat { get; init; }

    [JsonPropertyName("lng")]
    public double Lng { get; init; }
}