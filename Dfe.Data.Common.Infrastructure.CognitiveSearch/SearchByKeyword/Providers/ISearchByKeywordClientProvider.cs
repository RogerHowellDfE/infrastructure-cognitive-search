using Azure.Search.Documents;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword.Providers;

/// <summary>
/// 
/// </summary>
public interface ISearchByKeywordClientProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="indexName"></param>
    /// <returns></returns>
    Task<SearchClient> InvokeSearchClientAsync(string indexName);
}
