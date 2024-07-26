using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Model;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation;

/// <summary>
/// The contract defining the search by location behaviour.
/// </summary>
public interface IGeoLocationService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="location">
    /// 
    /// </param>
    /// <returns></returns>
    Task<GeoLocationServiceResponse> SearchGeoLocationAsync(string location);
}