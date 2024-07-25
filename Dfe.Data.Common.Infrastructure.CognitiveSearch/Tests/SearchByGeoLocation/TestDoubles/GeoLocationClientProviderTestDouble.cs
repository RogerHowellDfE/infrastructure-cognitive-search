using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Providers;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles;
using Moq;
using System.Net;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.SearchByGeoLocation.TestDoubles;

internal static class GeoLocationClientProviderTestDouble
{
    public static Mock<IGeoLocationClientProvider> IGeoLocationClientProviderMock() => new();

    public static IGeoLocationClientProvider CreateWithHttpStatusAndResponse(HttpStatusCode httpStatusCode, string response = "")
    {
        const string GeoLocationClientAddress = "https://atlas.microsoft.com/";
        var geoLocationClientProviderMock = IGeoLocationClientProviderMock();

        HttpClient httpClient =
            HttpClientTestDouble
                .CreateHttpClientWithMessageHandlerMock(
                    httpStatusCode, GeoLocationClientAddress, response);

        TaskCompletionSource<HttpClient> taskCompletion = new();
        taskCompletion.SetResult(httpClient);

        geoLocationClientProviderMock.Setup(_
            => _.InvokeGeoLocationClientAsync()).Returns(taskCompletion.Task);

        return geoLocationClientProviderMock.Object;
    }
}
