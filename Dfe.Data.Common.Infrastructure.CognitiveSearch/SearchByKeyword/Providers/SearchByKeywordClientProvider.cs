using Azure;
using Azure.Search.Documents;
using Microsoft.Extensions.Options;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword.Providers;

/// <summary>
/// 
/// </summary>
public sealed class SearchByKeywordClientProvider : ISearchByKeywordClientProvider
{
    private readonly Options.SearchByKeywordClientOptions _azureSearchClientOptions;
    private readonly Dictionary<string, Lazy<SearchClient>> _lazySearchClients;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="azureSearchOptions"></param>
    /// <param name="indexNamesProvider"></param>
    public SearchByKeywordClientProvider(
        IOptions<Options.SearchByKeywordClientOptions> azureSearchOptions,
        ISearchIndexNamesProvider indexNamesProvider)
    {
        ArgumentNullException.ThrowIfNull(azureSearchOptions);

        _azureSearchClientOptions = azureSearchOptions.Value;
        _lazySearchClients = [];

        indexNamesProvider
            .GetIndexNames()
            .ToList()
            .ForEach(indexName =>
            {
                _lazySearchClients.Add(
                    indexName,
                     new Lazy<SearchClient>(() =>
                        CreateSearchClientInstance(indexName)));
            });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="indexName"></param>
    /// <returns></returns>
    /// <exception cref="SearchByKeywordClientInvocationException"></exception>
    public Task<SearchClient> InvokeSearchClientAsync(string indexName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(indexName);

        _lazySearchClients.
            TryGetValue(indexName, out Lazy<SearchClient>? _searchClient);

        if (_searchClient == null)
        {
            throw new SearchByKeywordClientInvocationException(indexName);
        }

        return Task.FromResult(_searchClient.Value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="indexName"></param>
    /// <returns></returns>
    private SearchClient CreateSearchClientInstance(string indexName)
    {
        ArgumentNullException.ThrowIfNull(indexName);
        ArgumentException.ThrowIfNullOrEmpty(_azureSearchClientOptions.EndpointUri);
        ArgumentException.ThrowIfNullOrEmpty(_azureSearchClientOptions.Credentials);

        return new SearchClient(
            endpoint: new Uri(
                _azureSearchClientOptions.EndpointUri),
            indexName: indexName,
            credential: new AzureKeyCredential(
                _azureSearchClientOptions.Credentials));
    }
}
