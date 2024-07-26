namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Providers;

/// <summary>
/// Defines the contract for establishing a configured HttpClient for use when making geo-location service requests.
/// </summary>
public interface IGeoLocationClientProvider
{
    /// <summary>
    /// Creates an instance of a configured HttpClient in support of geo-location requests.
    /// </summary>
    /// <returns></returns>
    Task<HttpClient> InvokeGeoLocationClientAsync();
}
