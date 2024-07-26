using System.Text.Json.Serialization;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Model;

/// <summary>
/// Response object which encapsulates all T:Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Model.GeoLocationSearchResult
/// search results associated with any given search by geo-location request.
/// </summary>
public class GeoLocationServiceResponse
{
    /// <summary>
    /// The collection of results (if any) associated with the given search by geo-location request.
    /// </summary>
    [JsonPropertyName("results")]
    public GeoLocationSearchResult[]? GeoLocationSearchResults { get; set; }
}
