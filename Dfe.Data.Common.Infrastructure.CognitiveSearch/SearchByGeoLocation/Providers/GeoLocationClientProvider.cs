namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Providers;

/// <summary>
/// Provides a readily configured HttpClient (setup and registered in the native DI container)
/// for use when making geo-location service requests. The provider makes use of the IHttpClientFactory
/// which manages the lifetime of HttpMessageHandler and so avoids problems/issues that can occur when
/// managing HttpClient lifetimes directly.
/// </summary>
public class GeoLocationClientProvider : IGeoLocationClientProvider
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// The geo-location client provider uses an HttpClient created by the registered IHttpClientFactory.
    /// This factory assigns an HttpMessageHandler from a pool to the HttpClient. The HttpClient is configured
    /// when registering the IHttpClientFactory in the DI container with the extension method AddHttpClient.
    /// </summary>
    /// <param name="httpClientFactory">
    /// The httpClientFactory implementation defined with the native DI container.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Exception type thrown if a configured httpClientFactory is not provided.
    /// </exception>
    public GeoLocationClientProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ??
             throw new ArgumentNullException(nameof(httpClientFactory));
    }

    /// <summary>
    /// Creates an instance of the required HttpClient using the prescribed
    /// GeoLocationHttpClientKey to target the expected client registered and
    /// configured within the native DI container.
    /// </summary>
    /// <returns>
    /// A configured instance of the HttpClient specified (via GeoLocationHttpClientKey).
    /// </returns>
    /// <exception cref="GeoLocationClientInvocationException"></exception>
    public Task<HttpClient> InvokeGeoLocationClientAsync()
    {
        var geoLocationClient =
            _httpClientFactory.CreateClient(GeoLocationHttpClientKey) ??
                throw new GeoLocationClientInvocationException(GeoLocationHttpClientKey);

        return Task.FromResult(geoLocationClient);
    }

    /// <summary>
    /// Key used to acquire a targeted instance of a specific HttpClient
    /// configured in support of geo-location requests.
    /// </summary>
    private const string GeoLocationHttpClientKey = "GeoLocationHttpClient";
}