using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Search.Model;

/// <summary>
/// 
/// </summary>
[DataContract(Name = "Result")]
public class GeoLocationSearchResult
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("address")]
    public Address? GeographicalAddress { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("position")]
    public GeographicalPosition? GeographicalPosition { get; set; }
}
