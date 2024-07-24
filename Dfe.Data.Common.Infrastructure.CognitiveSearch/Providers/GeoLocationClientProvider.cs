namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Providers;

/// <summary>
/// 
/// </summary>
public class GeoLocationClientProvider : IGeoLocationClientProvider
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpClientFactory"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public GeoLocationClientProvider(IHttpClientFactory httpClientFactory)
    {
       _httpClientFactory = httpClientFactory ??
            throw new ArgumentNullException(nameof(httpClientFactory));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="GeoLocationClientInvocationException"></exception>
    public Task<HttpClient> InvokeGeoLocationClientAsync()
    {
        var geoLocationClient =
            _httpClientFactory.CreateClient(GeoLocationHttpClientKey) ??
                throw new GeoLocationClientInvocationException(GeoLocationHttpClientKey);

        return Task.FromResult(geoLocationClient);
    }

    /// <summary>
    /// 
    /// </summary>
    private const string GeoLocationHttpClientKey = "GeoLocationHttpClient";
}