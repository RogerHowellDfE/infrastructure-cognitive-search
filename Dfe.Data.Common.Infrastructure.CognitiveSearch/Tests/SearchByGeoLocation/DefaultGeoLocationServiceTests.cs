using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Model;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Options;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Providers;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.SearchByGeoLocation.Resources;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.SearchByGeoLocation.TestDoubles;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.SearchByGeoLocation.TestDoubles.StubBuilders;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System.Net;
using Xunit;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.SearchByGeoLocation
{
    public class DefaultGeoLocationServiceTests
    {
        [Fact]
        public void DefaultGeoLocationService_With_Null_GeoLocationClientProvider_Constructor_Arg_Throws_Expected_ArgumentNullException()
        {
            // arrange
            IGeoLocationClientProvider? geoLocationClientProvider = null;
            var geoLocationOptions = new GeoLocationOptionsBuilder().Create();
            var options = IOptionsTestDouble.IOptionsMockFor(geoLocationOptions);

            // act, assert
            ArgumentException exception =
                Assert.Throws<ArgumentNullException>(() =>
                    new DefaultGeoLocationService(geoLocationClientProvider!, options));

            Assert.Equal("Value cannot be null. (Parameter 'geoLocationClientProvider')", exception.Message);
        }

        [Fact]
        public async Task SearchGeoLocationAsync_With_Negative_Succes_Status_Throw_Expected_Exception()
        {
            // arrange
            IGeoLocationClientProvider? geoLocationClientProvider =
                GeoLocationClientProviderTestDouble.CreateWithHttpStatusAndResponse(HttpStatusCode.NotFound);
            var geoLocationOptions = new GeoLocationOptionsBuilder().Create();
            var options = IOptionsTestDouble.IOptionsMockFor(geoLocationOptions);

            // act, assert
            _ = new DefaultGeoLocationService(geoLocationClientProvider, options)
                .Invoking(async service =>
                    await service.SearchGeoLocationAsync("Invalid"))
                        .Should()
                            .ThrowAsync<HttpRequestException>()
                            .WithMessage("Response status code does not indicate success: 404 (Not Found).");
        }

        [Fact]
        public void DefaultGeoLocationService_With_Null_GeoLocationOptions_Constructor_Arg_Throws_Expected_ArgumentNullException()
        {
            // arrange
            IGeoLocationClientProvider? geoLocationClientProvider =
                GeoLocationClientProviderTestDouble.CreateWithHttpStatusAndResponse(HttpStatusCode.OK);
            IOptions<GeoLocationOptions>? options = null;

            // act, assert
            ArgumentException exception =
                Assert.Throws<ArgumentNullException>(() =>
                    new DefaultGeoLocationService(geoLocationClientProvider, options!));

            Assert.Equal("Value cannot be null. (Parameter 'geoLocationOptions')", exception.Message);
        }

        [Fact]
        public async Task SearchGeoLocationAsync_With_Null_Param_Throws_Expected_ArgumentNullException()
        {
            // arrange
            IGeoLocationClientProvider? geoLocationClientProvider =
                GeoLocationClientProviderTestDouble.CreateWithHttpStatusAndResponse(HttpStatusCode.OK);
            var geoLocationOptions = new GeoLocationOptionsBuilder().Create();
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
                GeoLocationClientProviderTestDouble.CreateWithHttpStatusAndResponse(HttpStatusCode.OK);
            var geoLocationOptions = new GeoLocationOptionsBuilder().Create();
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
            string rawJson = await JsonFileLoader.LoadJsonFile("GeoLocationSearchResponse.json");

            IGeoLocationClientProvider? geoLocationClientProvider =
                GeoLocationClientProviderTestDouble.CreateWithHttpStatusAndResponse(HttpStatusCode.OK, rawJson);
            var geoLocationOptions =
                new GeoLocationOptionsBuilder().Create();
            var options = IOptionsTestDouble.IOptionsMockFor(geoLocationOptions);

            // act, assert
            var result =
                 await new DefaultGeoLocationService(
                     geoLocationClientProvider, options).SearchGeoLocationAsync("Valid");

            Assert.NotNull(result);
            Assert.NotNull(result.GeoLocationSearchResults);
            Assert.IsType<GeoLocationServiceResponse>(result);
        }
    }
}
