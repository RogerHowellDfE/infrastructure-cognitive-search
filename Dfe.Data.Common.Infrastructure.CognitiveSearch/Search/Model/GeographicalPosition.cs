using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Search.Model;

/// <summary>
/// 
/// </summary>
[DataContract(Name = "Position")]
public sealed class GeographicalPosition
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("lat")]
    public float Latitude { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("lon")]
    public float Longitude { get; set; }
}
