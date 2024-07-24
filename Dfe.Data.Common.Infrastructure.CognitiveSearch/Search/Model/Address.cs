using System.Runtime.Serialization;
using System.Text.Json.Serialization;

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
    [JsonPropertyName("municipality")]
    public string? Town { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("countrySecondarySubdivision")]
    public string? County { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("countrySubdivisionName")]
    public string? CountryName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("countrySubdivisionCode")]
    public string? CountryCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("freeformAddress")]
    public string? FreeFormAddress { get; set; }
}
