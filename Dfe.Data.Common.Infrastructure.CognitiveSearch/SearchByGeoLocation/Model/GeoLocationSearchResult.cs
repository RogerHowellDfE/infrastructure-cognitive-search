using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Model;

/// <summary>
/// 
/// </summary>
[DataContract(Name = "Result")]
public class GeoLocationSearchResult
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("address")]
    public Address? GeographicalAddress { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("position")]
    public GeographicalPosition? GeographicalPosition { get; set; }
}
