using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Options;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.SearchByGeoLocation.TestDoubles.StubBuilders;

internal class GeoLocationOptionsBuilder
{
    private readonly string _mapServiceUri = Faker.Internet.SecureUrl();
    private readonly string _searchEndpointUri = Faker.Internet.SecureUrl();
    private readonly string _mapsSubscriptionKey = Faker.Identification.BulgarianPin();

    public GeoLocationOptions Create() =>
        new()
        {
            MapsServiceUri = _mapServiceUri,
            SearchEndpointUri = _searchEndpointUri,
            MapsSubscriptionKey = _mapsSubscriptionKey
        };
}
