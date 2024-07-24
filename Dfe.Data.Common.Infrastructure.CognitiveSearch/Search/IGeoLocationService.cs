using Dfe.Data.Common.Infrastructure.CognitiveSearch.Search.Model;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Search;

/// <summary>
/// 
/// </summary>
public interface IGeoLocationService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    Task<GeoLocationServiceResponse> SearchGeoLocationAsync(string location);
}