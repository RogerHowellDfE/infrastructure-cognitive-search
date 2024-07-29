using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Model;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation;

/// <summary>
/// The contract defining the search by location behaviour.
/// </summary>
public interface IGeoLocationService
{
    /// <summary>
    /// Describes the contract for calling to the underlying azure
    /// location service and defines the response type.
    /// </summary>
    /// <param name="location">
    /// The location string on which to establish the basis of the search.
    /// </param>
    /// <returns>
    /// A configured T:Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Model.GeoLocationServiceResponse instance.
    /// </returns>
    Task<GeoLocationServiceResponse> SearchGeoLocationAsync(string location);
}