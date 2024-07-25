using Moq;
using Moq.Protected;
using System.Net;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles;

internal static class HttpClientTestDouble
{
    public static Mock<HttpMessageHandler> HttpMessageHandlerMock = new();

    public static HttpClient CreateHttpClientWithMessageHandlerMock(
        HttpStatusCode httpStatusCode, string clientBaseAddress, string rawResponse)
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
                    Content = new StringContent(rawResponse)
                });

        return new HttpClient(HttpMessageHandlerMock.Object){
            BaseAddress = new Uri(clientBaseAddress)
        };
    }
}
