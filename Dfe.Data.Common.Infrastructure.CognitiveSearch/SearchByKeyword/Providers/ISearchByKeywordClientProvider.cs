using Azure.Search.Documents;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword.Providers;

/// <summary>
/// Provides a contract by which to derive (by index name)
/// a configured T:Azure.Search.Documents.SearchClient
/// for use when making search by key-word requests.
/// </summary>
public interface ISearchByKeywordClientProvider
{
    /// <summary>
    /// Defined the contract for invoking a search client instance based on the index name specified.
    /// </summary>
    /// <param name="indexName">
    /// The string name of the index under which to derive a configured search client instance.
    /// </param>
    /// <returns>
    /// A search client instance configured to search across the index (name) specified.
    /// </returns>
    Task<SearchClient> InvokeSearchClientAsync(string indexName);
}
