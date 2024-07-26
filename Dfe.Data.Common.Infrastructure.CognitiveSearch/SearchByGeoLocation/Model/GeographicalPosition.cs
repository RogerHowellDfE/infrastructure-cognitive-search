using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Model;

/// <summary>
/// Provides a type for capturing the geo-graphical position of a search location response.
/// </summary>
[DataContract(Name = "Position")]
public sealed class GeographicalPosition
{
    /// <summary>
    /// A number that represents the latitude associated with the location search result.
    /// </summary>
    [JsonPropertyName("lat")]
    public float Latitude { get; set; }

    /// <summary>
    /// A number that represents the longitude associated with the location search result.
    /// </summary>
    [JsonPropertyName("lon")]
    public float Longitude { get; set; }
}
