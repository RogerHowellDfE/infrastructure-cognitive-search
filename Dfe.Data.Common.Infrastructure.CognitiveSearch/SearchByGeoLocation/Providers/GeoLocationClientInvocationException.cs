namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByGeoLocation.Providers;

/// <summary>
/// Custom exception used to indicate an error has occurred invoking a geo-location HttpClient.
/// </summary>
public sealed class GeoLocationClientInvocationException : ApplicationException
{
    /// <summary>
    /// Constructor specifies the client name key used in an attempt to extract the correct
    /// geo-location HttpClient from the registered DI container. In the case of failure it
    /// is likely that either the client does not exist (i.e. has not been correctly registered)
    /// or an incorrect client name key has been specified.
    /// </summary>
    /// <param name="clientNameKey">
    /// The name key of the HttpClient used in an attempt to extract the correct client instance from the DI container.
    /// </param>
    public GeoLocationClientInvocationException(string clientNameKey)
        : base($"An error occurred invoking the geo-location client with client name: {clientNameKey}."){
    }
}