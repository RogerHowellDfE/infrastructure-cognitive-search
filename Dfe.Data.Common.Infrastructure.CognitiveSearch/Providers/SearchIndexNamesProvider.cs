using Azure;
using Azure.Search.Documents.Indexes;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Options;
using Microsoft.Extensions.Options;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Providers;

/// <summary>
/// 
/// </summary>
public sealed class SearchIndexNamesProvider : ISearchIndexNamesProvider
{
    private readonly AzureSearchClientOptions _azureSearchOptions;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="azureSearchOptions"></param>
    public SearchIndexNamesProvider(IOptions<AzureSearchClientOptions> azureSearchOptions)
    {
        ArgumentNullException.ThrowIfNull(azureSearchOptions);

        _azureSearchOptions = azureSearchOptions.Value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetIndexNames()
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(_azureSearchOptions.EndpointUri);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(_azureSearchOptions.Credentials);

        List<string> indexNames = [];

        SearchIndexClient client =
            new(
                endpoint: new Uri(_azureSearchOptions.EndpointUri),
                credential: new AzureKeyCredential(_azureSearchOptions.Credentials));

        client.GetIndexNames().ToList()
            .ForEach(index => indexNames.Add(index));

        return indexNames;
    }
}
