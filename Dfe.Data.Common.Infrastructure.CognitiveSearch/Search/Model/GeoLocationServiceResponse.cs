using Newtonsoft.Json;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Search.Model;

/// <summary>
/// 
/// </summary>
public class GeoLocationServiceResponse
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("results")]
    public GeoLocationSearchResult[]? GeoLocationSearchResults { get; set; }
}
