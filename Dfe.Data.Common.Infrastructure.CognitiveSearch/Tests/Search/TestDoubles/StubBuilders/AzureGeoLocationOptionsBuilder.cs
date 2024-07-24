using Dfe.Data.Common.Infrastructure.CognitiveSearch.Options;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles.StubBuilders;

internal class AzureGeoLocationOptionsBuilder
{
    private readonly string _mapServiceUri = Faker.Internet.SecureUrl();
    private readonly string _searchEndpointUri = Faker.Internet.SecureUrl();
    private readonly string _mapsSubscriptionKey = Faker.Identification.BulgarianPin();

    public AzureGeoLocationOptions Create() =>
        new()
        {
            MapsServiceUri = _mapServiceUri,
            SearchEndpointUri = _searchEndpointUri,
            MapsSubscriptionKey = _mapsSubscriptionKey
        };
}
