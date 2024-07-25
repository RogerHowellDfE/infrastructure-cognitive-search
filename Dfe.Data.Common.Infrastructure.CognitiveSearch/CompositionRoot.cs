using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Options;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Providers;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword.Options;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword.Providers;
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
    public static void AddDefaultCognitiveSearchServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services),
                "A service collection is required to configure the azure cognitive search dependencies.");
        }

        services.TryAddSingleton<ISearchByKeywordClientProvider, SearchByKeywordClientProvider>();
        services.TryAddSingleton<ISearchIndexNamesProvider, SearchIndexNamesProvider>();
        services.TryAddSingleton<ISearchByKeywordService, DefaultSearchByKeywordService>();
        services.TryAddScoped<IGeoLocationClientProvider, GeoLocationClientProvider>();
        services.TryAddScoped<IGeoLocationService, DefaultGeoLocationService>();

        services.AddOptions<SearchByKeywordClientOptions>()
           .Configure<IConfiguration>(
               (settings, configuration) =>
                   configuration
                       .GetSection(nameof(SearchByKeywordClientOptions))
                       .Bind(settings));

        services.AddOptions<GeoLocationOptions>()
           .Configure<IConfiguration>(
               (settings, configuration) =>
                   configuration
                       .GetSection(nameof(GeoLocationOptions))
                       .Bind(settings));

        services.AddHttpClient("GeoLocationHttpClient", config =>
        {
            var geoLocationOptions =
                configuration
                    .GetSection(nameof(GeoLocationOptions)).Get<GeoLocationOptions>();

            ArgumentNullException.ThrowIfNull(geoLocationOptions);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(geoLocationOptions.MapsServiceUri);

            config.BaseAddress = new Uri(geoLocationOptions.MapsServiceUri);
            config.Timeout = new TimeSpan(0, 0, 30); // TODO: get this from config?
            config.DefaultRequestHeaders.Clear();
        });
    }
}
