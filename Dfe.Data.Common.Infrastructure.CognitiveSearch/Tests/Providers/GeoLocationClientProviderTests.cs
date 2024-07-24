using Dfe.Data.Common.Infrastructure.CognitiveSearch.Providers;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles;
using Xunit;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Providers
{
    public class GeoLocationClientProviderTests
    {
        [Fact]
        public async Task InvokeGeoLocationClientAsync_WithValidClientFactoryKey_ReturnsGeoLocationClient()
        {
            // arrange
            const string ValidGeoLocationHttpClientKey = "GeoLocationHttpClient";
            const string GeoLocationClientAddress = "https://atlas.microsoft.com/";

            IHttpClientFactory clientFactory =
                HttpClientFactoryTestDouble
                    .CreateHttpClientFactoryMockFor(ValidGeoLocationHttpClientKey, GeoLocationClientAddress);

            var geoLocationClientProvider = new GeoLocationClientProvider(clientFactory);

            // act
            var result = await geoLocationClientProvider.InvokeGeoLocationClientAsync();

            // assert
            Assert.NotNull(result);
            Assert.Equal("https://atlas.microsoft.com/", result.BaseAddress!.ToString());
        }

        [Fact]
        public void InvokeGeoLocationClientAsync_WithInvalidClientFactoryKey_ThrowsGeoLocationClientInvocationException()
        {
            // arrange
            const string InvalidGeoLocationHttpClientKey = "BadHttpClientKey";
            const string GeoLocationClientAddress = "https://atlas.microsoft.com/";

            IHttpClientFactory clientFactory =
                HttpClientFactoryTestDouble
                    .CreateHttpClientFactoryMockFor(InvalidGeoLocationHttpClientKey, GeoLocationClientAddress);

            var geoLocationClientProvider = new GeoLocationClientProvider(clientFactory);

            // act
            Action failedGeoLocationClientAction =  () =>
                 geoLocationClientProvider.InvokeGeoLocationClientAsync();

            // assert
            ApplicationException exception =
                Assert.Throws<GeoLocationClientInvocationException>(failedGeoLocationClientAction);
            Assert.Equal("An error occurred invoking the geo-location client with client name: GeoLocationHttpClient.", exception.Message);
        }
    }
}
