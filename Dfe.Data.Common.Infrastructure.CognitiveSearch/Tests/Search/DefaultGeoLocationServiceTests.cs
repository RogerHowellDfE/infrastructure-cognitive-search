using Dfe.Data.Common.Infrastructure.CognitiveSearch.Options;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Providers;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Search;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Search.Model;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles.StubBuilders;
using Microsoft.Extensions.Options;
using System.Net;
using Xunit;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search
{
    public class DefaultGeoLocationServiceTests
    {
        [Fact]
        public void DefaultGeoLocationService_With_Null_GeoLocationClientProvider_Constructor_Arg_Throws_Expected_ArgumentNullException()
        {
            // arrange
            IGeoLocationClientProvider? geoLocationClientProvider = null;
            var geoLocationOptions = new AzureGeoLocationOptionsBuilder().Create();
            var options = IOptionsTestDouble.IOptionsMockFor(geoLocationOptions);

            // act, assert
            ArgumentException exception =
                Assert.Throws<ArgumentNullException>(() =>
                    new DefaultGeoLocationService(geoLocationClientProvider!, options));

            Assert.Equal("Value cannot be null. (Parameter 'geoLocationClientProvider')", exception.Message);
        }

        [Fact]
        public void DefaultGeoLocationService_With_Null_GeoLocationOptions_Constructor_Arg_Throws_Expected_ArgumentNullException()
        {
            // arrange
            IGeoLocationClientProvider? geoLocationClientProvider =
                GeoLocationClientProviderTestDouble.CreateWithHttpStatus(HttpStatusCode.OK);
            IOptions<AzureGeoLocationOptions>? options = null;

            // act, assert
            ArgumentException exception =
                Assert.Throws<ArgumentNullException>(() =>
                    new DefaultGeoLocationService(geoLocationClientProvider, options!));

            Assert.Equal("Value cannot be null. (Parameter 'azureGeoLocationOptions')", exception.Message);
        }

        [Fact]
        public async Task SearchGeoLocationAsync_With_Null_Param_Throws_Expected_ArgumentNullException()
        {
            // arrange
            IGeoLocationClientProvider? geoLocationClientProvider =
                GeoLocationClientProviderTestDouble.CreateWithHttpStatus(HttpStatusCode.OK);
            var geoLocationOptions = new AzureGeoLocationOptionsBuilder().Create();
            var options = IOptionsTestDouble.IOptionsMockFor(geoLocationOptions);

            // act, assert
            ArgumentNullException exception =
                 await Assert.ThrowsAsync<ArgumentNullException>(() =>
                     new DefaultGeoLocationService(geoLocationClientProvider, options)
                        .SearchGeoLocationAsync(null!));

            Assert.Equal("Value cannot be null. (Parameter 'location')", exception.Message);
        }

        [Fact]
        public async Task SearchGeoLocationAsync_With_Empty_Param_Throws_Expected_ArgumentNullException()
        {
            // arrange
            IGeoLocationClientProvider? geoLocationClientProvider =
                GeoLocationClientProviderTestDouble.CreateWithHttpStatus(HttpStatusCode.OK);
            var geoLocationOptions = new AzureGeoLocationOptionsBuilder().Create();
            var options = IOptionsTestDouble.IOptionsMockFor(geoLocationOptions);

            // act, assert
            ArgumentException exception =
                 await Assert.ThrowsAsync<ArgumentException>(() =>
                     new DefaultGeoLocationService(geoLocationClientProvider, options)
                        .SearchGeoLocationAsync(string.Empty));

            Assert.Equal("The value cannot be an empty string. (Parameter 'location')", exception.Message);
        }

        [Fact]
        public async Task SearchGeoLocationAsync_With_Positive_Succes_Status_Return_Expected_Result()
        {
            // arrange
            IGeoLocationClientProvider? geoLocationClientProvider =
                GeoLocationClientProviderTestDouble.CreateWithHttpStatus(HttpStatusCode.OK);
            var geoLocationOptions =
                new AzureGeoLocationOptionsBuilder().Create();
            var options = IOptionsTestDouble.IOptionsMockFor(geoLocationOptions);

            // act, assert
            var result =
                 await new DefaultGeoLocationService(
                     geoLocationClientProvider, options).SearchGeoLocationAsync("Valid");

            Assert.NotNull(result);
            Assert.NotNull(result.GeoLocationSearchResults);
            Assert.IsType<GeoLocationServiceResponse>(result);
        }

        [Fact]
        public async Task SearchGeoLocationAsync_With_Negative_Succes_Status_Throw_Expected_Exception()
        {
            // arrange
            IGeoLocationClientProvider? geoLocationClientProvider =
                GeoLocationClientProviderTestDouble.CreateWithHttpStatus(HttpStatusCode.NotFound);
            var geoLocationOptions =
                new AzureGeoLocationOptionsBuilder().Create();
            var options = IOptionsTestDouble.IOptionsMockFor(geoLocationOptions);

            // act, assert
            HttpRequestException exception =
                 await Assert.ThrowsAsync<HttpRequestException>(() =>
                     new DefaultGeoLocationService(geoLocationClientProvider, options)
                        .SearchGeoLocationAsync("Invalid"));

            Assert.Equal("Response status code does not indicate success: 404 (Not Found).", exception.Message);
        }
    }
}
