using Moq;
using Moq.Protected;
using System.Net;
using System.Text;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles;

internal static class HttpClientTestDouble
{
    public static Mock<HttpMessageHandler> HttpMessageHandlerMock = new();

    public static HttpClient CreateHttpClientWithMessageHandlerMock(
        HttpStatusCode httpStatusCode, string clientBaseAddress)
    {
        HttpMessageHandlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(
                new HttpResponseMessage{
                    StatusCode = httpStatusCode,
                    Content = new StringContent(RawResonse)
                });

        return new HttpClient(HttpMessageHandlerMock.Object){
            BaseAddress = new Uri(clientBaseAddress)
        };
    }

    const string RawResonse =
        "{\"summary\":{\"query\":\"15127NE24thStreet,Redmond,WA98052\",\"queryType\":\"NON_NEAR\",\"queryTime\":58,\"numResults\":1,\"offset\":0,\"totalResults\":1,\"fuzzyLevel\":1},\"results\":[{\"type\":\"PointAddress\",\"id\":\"US/PAD/p0/19173426\",\"score\":14.51,\"address\":{\"streetNumber\":\"15127\",\"streetName\":\"NE24thSt\",\"municipalitySubdivision\":\"Redmond\",\"municipality\":\"Redmond,Adelaide,AmesLake,Avondale,Earlmount\",\"countrySecondarySubdivision\":\"King\",\"countryTertiarySubdivision\":\"SeattleEast\",\"countrySubdivisionCode\":\"WA\",\"postalCode\":\"98052\",\"extendedPostalCode\":\"980525544\",\"countryCode\":\"US\",\"country\":\"UnitedStatesOfAmerica\",\"countryCodeISO3\":\"USA\",\"freeformAddress\":\"15127NE24thSt,Redmond,WA980525544\",\"countrySubdivisionName\":\"Washington\"},\"position\":{\"lat\":47.6308,\"lon\":-122.1385},\"viewport\":{\"topLeftPoint\":{\"lat\":47.6317,\"lon\":-122.13983},\"btmRightPoint\":{\"lat\":47.6299,\"lon\":-122.13717}},\"entryPoints\":[{\"type\":\"main\",\"position\":{\"lat\":47.6315,\"lon\":-122.13852}}]}]}";
}
