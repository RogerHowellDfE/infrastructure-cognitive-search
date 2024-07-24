using Dfe.Data.Common.Infrastructure.CognitiveSearch.Options;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Providers;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Search;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch;

/// <summary>
/// 
/// </summary>
public static class CompositionRoot
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void AddAzureCognitiveSearchProvider(this IServiceCollection services, IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services),
                "A service collection is required to configure the azure cognitive search dependencies.");
        }

        services.TryAddSingleton<ISearchClientProvider, AzureSearchClientProvider>();
        services.TryAddSingleton<ISearchIndexNamesProvider, SearchIndexNamesProvider>();
        services.TryAddSingleton<ISearchService, DefaultSearchService>();
        services.TryAddScoped<IGeoLocationClientProvider, GeoLocationClientProvider>();
        services.TryAddScoped<IGeoLocationService, DefaultGeoLocationService>();

        services.AddOptions<AzureSearchClientOptions>()
           .Configure<IConfiguration>(
               (settings, configuration) =>
                   configuration
                       .GetSection("AzureCognitiveSearchOptions:AzureSearchClientOptions")
                       .Bind(settings));

        services.AddOptions<AzureGeoLocationOptions>()
           .Configure<IConfiguration>(
               (settings, configuration) =>
                   configuration
                       .GetSection(nameof(AzureGeoLocationOptions))
                       .Bind(settings));

        services.AddHttpClient("GeoLocationHttpClient", config =>
        {
            var azureGeoLocationOptions =
                configuration
                    .GetSection("AzureGeoLocationOptions").Get<AzureGeoLocationOptions>();

            ArgumentNullException.ThrowIfNull(azureGeoLocationOptions);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(azureGeoLocationOptions.MapsServiceUri);

            config.BaseAddress = new Uri(azureGeoLocationOptions.MapsServiceUri);
            config.Timeout = new TimeSpan(0, 0, 30);
            config.DefaultRequestHeaders.Clear();
        });
    }
}
