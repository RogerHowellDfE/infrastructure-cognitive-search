namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Providers;

/// <summary>
/// 
/// </summary>
public interface IGeoLocationClientProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<HttpClient> InvokeGeoLocationClientAsync();
}
