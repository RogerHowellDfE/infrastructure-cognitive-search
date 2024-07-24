using Dfe.Data.Common.Infrastructure.CognitiveSearch.Options;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Providers;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Search.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Search;

/// <summary>
/// 
/// </summary>
public sealed class DefaultGeoLocationService : IGeoLocationService
{
    private readonly IGeoLocationClientProvider _geoLocationClientProvider;
    private readonly AzureGeoLocationOptions _azureGeoLocationOptions;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="geoLocationClientProvider"></param>
    /// <param name="azureGeoLocationOptions"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public DefaultGeoLocationService(
        IGeoLocationClientProvider geoLocationClientProvider,
        IOptions<AzureGeoLocationOptions> azureGeoLocationOptions)
    {
        _geoLocationClientProvider = geoLocationClientProvider ??
            throw new ArgumentNullException(nameof(geoLocationClientProvider));

        ArgumentNullException.ThrowIfNullOrEmpty(nameof(azureGeoLocationOptions));

        _azureGeoLocationOptions = azureGeoLocationOptions.Value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    /// <exception cref="JsonException"></exception>
    public Task<GeoLocationServiceResponse> SearchGeoLocationAsync(string location)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(location);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(_azureGeoLocationOptions.SearchEndpointUri);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(_azureGeoLocationOptions.MapsSubscriptionKey);

        return _geoLocationClientProvider.InvokeGeoLocationClientAsync().ContinueWith(
            async geoLocationClient =>
            {
                using var geoLocationResponse =
                      await geoLocationClient.Result.GetAsync(
                          string.Format(
                              _azureGeoLocationOptions.SearchEndpointUri, location,
                              _azureGeoLocationOptions.MapsSubscriptionKey),
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
