using Dfe.Data.Common.Infrastructure.CognitiveSearch.Providers;
using Moq;
using System.Net;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles;

internal static class GeoLocationClientProviderTestDouble
{
    public static Mock<IGeoLocationClientProvider> IGeoLocationClientProviderMock() => new();

    public static IGeoLocationClientProvider CreateWithHttpStatus(HttpStatusCode httpStatusCode)
    {
        const string GeoLocationClientAddress = "https://atlas.microsoft.com/";
        var geoLocationClientProviderMock = IGeoLocationClientProviderMock();

        TaskCompletionSource<HttpClient> taskCompletion = new();
        taskCompletion.SetResult(
            HttpClientTestDouble.CreateHttpClientWithMessageHandlerMock(httpStatusCode, GeoLocationClientAddress));

        geoLocationClientProviderMock.Setup(_
            => _.InvokeGeoLocationClientAsync()).Returns(taskCompletion.Task);

        return geoLocationClientProviderMock.Object;
    }
}
