using System.Text.Json.Serialization;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Search.Model;

/// <summary>
/// 
/// </summary>
public class GeoLocationServiceResponse
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("results")]
    public GeoLocationSearchResult[]? GeoLocationSearchResults { get; set; }
}
