namespace Dfe.Data.Common.Infrastructure.CognitiveSearchProviders;

/// <summary>
/// 
/// </summary>
public sealed class SearchClientInvocationException : ApplicationException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="indexKey"></param>
    public SearchClientInvocationException(string indexKey)
        : base($"An error occurred invoking the search client with index key: {indexKey}."){
    }
}