using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Search.Model;

/// <summary>
/// 
/// </summary>
[DataContract(Name = "Address")]
public sealed class Address
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("municipality")]
    public string? Town { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("countrySecondarySubdivision")]
    public string? County { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("countrySubdivisionName")]
    public string? CountryName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("countrySubdivisionCode")]
    public string? CountryCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("freeformAddress")]
    public string? FreeFormAddress { get; set; }
}
