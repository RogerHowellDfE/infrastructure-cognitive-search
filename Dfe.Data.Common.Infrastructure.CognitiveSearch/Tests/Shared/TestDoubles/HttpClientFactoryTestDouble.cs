using Moq;
using System.Net;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles;

internal static class HttpClientFactoryTestDouble
{
    public static Mock<IHttpClientFactory> IHttpClientFactoryMock() => new();

    public static IHttpClientFactory CreateHttpClientFactoryMockFor(string httpClientKey, string clientAddress, string response = "")
    {
        var httpClientFactoryMock = IHttpClientFactoryMock();

        httpClientFactoryMock.Setup(_ =>
            _.CreateClient(It.Is<string>(httpClientKey =>
                httpClientKey.Equals(httpClientKey))))
                    .Returns(GetHttpClient(httpClientKey, clientAddress, response)!);

        return httpClientFactoryMock.Object;
    }

    public static HttpClient? GetHttpClient(string httpClientKey, string clientAddress, string response) {
        return httpClientKey.Equals("GeoLocationHttpClient") ?
            HttpClientTestDouble.CreateHttpClientWithMessageHandlerMock(HttpStatusCode.OK, clientAddress, response) : null;
    }
}
