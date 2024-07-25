using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Model;

/// <summary>
/// 
/// </summary>
[DataContract(Name = "Position")]
public sealed class GeographicalPosition
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("lat")]
    public float Latitude { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("lon")]
    public float Longitude { get; set; }
}
