namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Providers;

/// <summary>
/// 
/// </summary>
public sealed class GeoLocationClientInvocationException : ApplicationException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="clientName"></param>
    public GeoLocationClientInvocationException(string clientName)
        : base($"An error occurred invoking the geo-location client with client name: {clientName}."){
    }
}