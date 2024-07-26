using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Model;

/// <summary>
/// Provides a type for encapsulating the geo-graphical search result based from a location request.
/// </summary>
[DataContract(Name = "Result")]
public class GeoLocationSearchResult
{
    /// <summary>
    /// Provides a type for capturing key address features of a search location response.
    /// </summary>
    [JsonPropertyName("address")]
    public Address? GeographicalAddress { get; set; }

    /// <summary>
    /// Provides a type for capturing the geo-graphical position of a search location response.
    /// </summary>
    [JsonPropertyName("position")]
    public GeographicalPosition? GeographicalPosition { get; set; }
}
