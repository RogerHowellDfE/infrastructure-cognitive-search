using Azure;
using Azure.Search.Documents;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Options;
using Dfe.Data.Common.Infrastructure.CognitiveSearchProviders;
using Microsoft.Extensions.Options;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Providers;

/// <summary>
/// 
/// </summary>
public sealed class AzureSearchClientProvider : ISearchClientProvider
{
    private readonly AzureSearchClientOptions _azureSearchClientOptions;
    private readonly Dictionary<string, Lazy<SearchClient>> _lazySearchClients;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="azureSearchOptions"></param>
    /// <param name="indexNamesProvider"></param>
    public AzureSearchClientProvider(
        IOptions<AzureSearchClientOptions> azureSearchOptions,
        ISearchIndexNamesProvider indexNamesProvider)
    {
        ArgumentNullException.ThrowIfNull(azureSearchOptions);

        _azureSearchClientOptions = azureSearchOptions.Value;
        _lazySearchClients = [];

        indexNamesProvider
            .GetIndexNames()
            .ToList()
            .ForEach(indexName => {
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
    /// <exception cref="SearchClientInvocationException"></exception>
    public Task<SearchClient> InvokeSearchClientAsync(string indexName)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(indexName);

        _lazySearchClients.
            TryGetValue(indexName, out Lazy<SearchClient>? _searchClient);

        if (_searchClient == null){
            throw new SearchClientInvocationException(indexName);
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
        ArgumentNullException.ThrowIfNullOrWhiteSpace(indexName);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(_azureSearchClientOptions.EndpointUri);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(_azureSearchClientOptions.Credentials);

        return new SearchClient(
            endpoint: new Uri(
                _azureSearchClientOptions.EndpointUri),
            indexName: indexName,
            credential: new AzureKeyCredential(
                _azureSearchClientOptions.Credentials));
    }
}
