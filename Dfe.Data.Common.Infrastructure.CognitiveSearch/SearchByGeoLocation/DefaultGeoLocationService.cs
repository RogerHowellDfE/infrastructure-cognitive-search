using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Model;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Options;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Providers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation;

/// <summary>
/// 
/// </summary>
public sealed class DefaultGeoLocationService : IGeoLocationService
{
    private readonly IGeoLocationClientProvider _geoLocationClientProvider;
    private readonly GeoLocationOptions _geoLocationOptions;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="geoLocationClientProvider"></param>
    /// <param name="geoLocationOptions"></param>
    public DefaultGeoLocationService(
        IGeoLocationClientProvider geoLocationClientProvider,
        IOptions<GeoLocationOptions> geoLocationOptions)
    {
        ArgumentNullException.ThrowIfNull(geoLocationOptions);
        ArgumentNullException.ThrowIfNull(geoLocationClientProvider);

        _geoLocationClientProvider = geoLocationClientProvider;
        _geoLocationOptions = geoLocationOptions.Value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    /// <exception cref="JsonException"></exception>
    public Task<GeoLocationServiceResponse> SearchGeoLocationAsync(string location)
    {
        ArgumentException.ThrowIfNullOrEmpty(location);
        ArgumentException.ThrowIfNullOrWhiteSpace(_geoLocationOptions.SearchEndpointUri);
        ArgumentException.ThrowIfNullOrWhiteSpace(_geoLocationOptions.MapsSubscriptionKey);

        return _geoLocationClientProvider.InvokeGeoLocationClientAsync().ContinueWith(
            async geoLocationClient =>
            {
                using var geoLocationResponse =
                      await geoLocationClient.Result.GetAsync(
                          string.Format(
                              _geoLocationOptions.SearchEndpointUri, location,
                              _geoLocationOptions.MapsSubscriptionKey),
                        HttpCompletionOption.ResponseHeadersRead)
                      .ConfigureAwait(false);

                geoLocationResponse.EnsureSuccessStatusCode();

                string rawGeoLocationResponse =
                    await geoLocationResponse.Content
                        .ReadAsStringAsync().ConfigureAwait(false);

                return JsonSerializer
                    .Deserialize<GeoLocationServiceResponse>(rawGeoLocationResponse) ??
                    throw new JsonException(
                        $"Unable to de-serialise the geo-location response: {rawGeoLocationResponse}");
            },
            TaskContinuationOptions.OnlyOnRanToCompletion).Result;
    }
}
