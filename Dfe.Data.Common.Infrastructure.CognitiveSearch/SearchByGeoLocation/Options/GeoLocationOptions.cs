namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Options;

/// <summary>
/// 
/// </summary>
public sealed class GeoLocationOptions
{
    /// <summary>
    /// 
    /// </summary>
    public string? MapsServiceUri { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? SearchEndpointUri { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? MapsSubscriptionKey { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int RequestTimeOutHours { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int RequestTimeOutMinutes { get; set; }

    // <summary>
    ///
    /// </summary>
    public int RequestTimeOutSeconds { get; set; }
}
