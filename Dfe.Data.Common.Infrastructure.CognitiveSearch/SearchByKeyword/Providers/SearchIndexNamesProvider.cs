using Azure;
using Azure.Search.Documents.Indexes;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword.Options;
using Microsoft.Extensions.Options;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword.Providers;

/// <summary>
/// 
/// </summary>
public sealed class SearchIndexNamesProvider : ISearchIndexNamesProvider
{
    private readonly SearchByKeywordClientOptions _azureSearchOptions;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="azureSearchOptions"></param>
    public SearchIndexNamesProvider(IOptions<SearchByKeywordClientOptions> azureSearchOptions)
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
        ArgumentException.ThrowIfNullOrWhiteSpace(_azureSearchOptions.EndpointUri);
        ArgumentException.ThrowIfNullOrWhiteSpace(_azureSearchOptions.Credentials);

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
