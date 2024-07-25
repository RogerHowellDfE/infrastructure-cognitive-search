using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Model;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Options;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Providers;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.SearchByGeoLocation.TestDoubles;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.SearchByGeoLocation.TestDoubles.StubBuilders;
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
            HttpRequestException exception =
                 await Assert.ThrowsAsync<HttpRequestException>(() =>
                     new DefaultGeoLocationService(geoLocationClientProvider, options)
                        .SearchGeoLocationAsync("Invalid"));

            Assert.Equal("Response status code does not indicate success: 404 (Not Found).", exception.Message);
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
            const string RawResonse =
                "{\"summary\":{\"query\":\"15127NE24thStreet,Redmond,WA98052\",\"queryType\":\"NON_NEAR\",\"queryTime\":58,\"numResults\":1,\"offset\":0,\"totalResults\":1,\"fuzzyLevel\":1},\"results\":[{\"type\":\"PointAddress\",\"id\":\"US/PAD/p0/19173426\",\"score\":14.51,\"address\":{\"streetNumber\":\"15127\",\"streetName\":\"NE24thSt\",\"municipalitySubdivision\":\"Redmond\",\"municipality\":\"Redmond,Adelaide,AmesLake,Avondale,Earlmount\",\"countrySecondarySubdivision\":\"King\",\"countryTertiarySubdivision\":\"SeattleEast\",\"countrySubdivisionCode\":\"WA\",\"postalCode\":\"98052\",\"extendedPostalCode\":\"980525544\",\"countryCode\":\"US\",\"country\":\"UnitedStatesOfAmerica\",\"countryCodeISO3\":\"USA\",\"freeformAddress\":\"15127NE24thSt,Redmond,WA980525544\",\"countrySubdivisionName\":\"Washington\"},\"position\":{\"lat\":47.6308,\"lon\":-122.1385},\"viewport\":{\"topLeftPoint\":{\"lat\":47.6317,\"lon\":-122.13983},\"btmRightPoint\":{\"lat\":47.6299,\"lon\":-122.13717}},\"entryPoints\":[{\"type\":\"main\",\"position\":{\"lat\":47.6315,\"lon\":-122.13852}}]}]}";


            IGeoLocationClientProvider? geoLocationClientProvider =
                GeoLocationClientProviderTestDouble.CreateWithHttpStatusAndResponse(HttpStatusCode.OK, RawResonse);
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
