using Azure.Search.Documents;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Providers;

/// <summary>
/// 
/// </summary>
public interface ISearchClientProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="indexName"></param>
    /// <returns></returns>
    Task<SearchClient> InvokeSearchClientAsync(string indexName);
}
