using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Model;

/// <summary>
/// Provides a type for capturing key address features of a search location response.
/// </summary>
[DataContract(Name = "Address")]
public sealed class Address
{
    /// <summary>
    /// String value representing the town/city associated with the location search result.
    /// </summary>
    [JsonPropertyName("municipality")]
    public string? Town { get; set; }

    /// <summary>
    /// String value representing the county associated with the location search result.
    /// </summary>
    [JsonPropertyName("countrySecondarySubdivision")]
    public string? County { get; set; }

    /// <summary>
    /// String value representing the name of the country associated with the location search result.
    /// </summary>
    [JsonPropertyName("countrySubdivisionName")]
    public string? CountryName { get; set; }

    /// <summary>
    /// String value representing the Country Code (Note: This is a two-letter code, not a country/region name)
    /// associated with the location search result.
    /// </summary>
    [JsonPropertyName("countrySubdivisionCode")]
    public string? CountryCode { get; set; }

    /// <summary>
    /// String value representing an address line formatted according to the formatting rules of a Result's
    /// country/region of origin, or in the case of a country/region, its full country/region name,
    /// associated with the location search result.
    /// </summary>
    [JsonPropertyName("freeformAddress")]
    public string? FreeFormAddress { get; set; }
}
