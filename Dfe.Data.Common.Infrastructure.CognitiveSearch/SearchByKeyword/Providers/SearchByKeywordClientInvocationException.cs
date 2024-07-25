namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword.Providers;

/// <summary>
/// 
/// </summary>
public sealed class SearchByKeywordClientInvocationException : ApplicationException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="indexKey"></param>
    public SearchByKeywordClientInvocationException(string indexKey)
        : base($"An error occurred invoking the search client with index key: {indexKey}.")
    {
    }
}