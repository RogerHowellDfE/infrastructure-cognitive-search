namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword.Providers;

/// <summary>
/// Custom exception used to indicate an error has occurred invoking a search by keyword client.
/// </summary>
public sealed class SearchByKeywordClientInvocationException : ApplicationException
{
    /// <summary>
    /// Constructor specifies the index key used to derive the expected search client.
    /// In the case of failure it is likely that either the client does not exist
    /// (i.e. has not been correctly registered) or an incorrect index key has been specified.
    /// </summary>
    /// <param name="indexKey">
    /// The index key specified when making a request to derive a search client.
    /// </param>
    public SearchByKeywordClientInvocationException(string indexKey)
        : base($"An error occurred invoking the search client with index key: {indexKey}."){
    }
}