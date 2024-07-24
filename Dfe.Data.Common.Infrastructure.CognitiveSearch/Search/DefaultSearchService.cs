using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Providers;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Search;

/// <summary>
/// 
/// </summary>
public sealed class DefaultSearchService : ISearchService
{
    private readonly ISearchClientProvider _searchClientProvider;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="searchClientProvider"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public DefaultSearchService(
        ISearchClientProvider searchClientProvider)
    {
        _searchClientProvider = searchClientProvider ??
            throw new ArgumentNullException(nameof(searchClientProvider));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSearchResult"></typeparam>
    /// <param name="searchKeyword"></param>
    /// <param name="searchIndex"></param>
    /// <param name="searchOptions"></param>
    /// <returns></returns>
    public Task<Response<SearchResults<TSearchResult>>> SearchAsync<TSearchResult>(
        string searchKeyword, string searchIndex, SearchOptions searchOptions)
        where TSearchResult : class
    {
        ArgumentNullException.ThrowIfNullOrEmpty(searchKeyword);
        ArgumentNullException.ThrowIfNullOrEmpty(searchIndex);
        ArgumentNullException.ThrowIfNull(searchOptions);

        return InvokeSearch(
            searchIndex, (searchClient) =>
                searchClient.SearchAsync<TSearchResult>(
                    searchKeyword, searchOptions).Result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="searchIndex"></param>
    /// <param name="searchAction"></param>
    /// <returns></returns>
    private Task<TResult> InvokeSearch<TResult>(
        string searchIndex, Func<SearchClient, TResult> searchAction) where TResult : class
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(searchIndex);

        return _searchClientProvider
            .InvokeSearchClientAsync(searchIndex)
            .ContinueWith(
                searchClient =>
                    searchAction(searchClient.Result),
                TaskContinuationOptions.OnlyOnRanToCompletion);
    }
}
