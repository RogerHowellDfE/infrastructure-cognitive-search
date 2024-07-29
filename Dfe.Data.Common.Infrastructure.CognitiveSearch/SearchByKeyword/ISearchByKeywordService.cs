using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword;

/// <summary>
/// Contract which defined the default behaviour for allowing
/// requests to made to the azure search services, by keyword.
/// </summary>
public interface ISearchByKeywordService
{
    /// <summary>
    /// Contract for defining the call available to the underlying azure search service client using
    /// the prescribed search keyword and options request, and returns a
    /// response which aggregate the search result(s), if nay.
    /// </summary>
    /// <typeparam name="TSearchResult">
    /// The type of search result into which to unpack the raw search response.
    /// </typeparam>
    /// <param name="searchKeyword">
    /// The string search keyword used across the prescribed index.
    /// </param>
    /// <param name="searchIndex">
    /// The name of the index on which to assign the search.
    /// </param>
    /// <param name="searchOptions">
    /// Options for <see cref="SearchClient.SearchAsync"/> that
    /// allow specifying filtering, sorting, faceting, paging, and other search
    /// query behaviors.
    /// </param>
    /// <returns>
    /// The response containing search results from an index.
    /// </returns>
    Task<Response<SearchResults<TSearchResult>>> SearchAsync<TSearchResult>(
        string searchKeyword, string searchIndex, SearchOptions searchOptions)
        where TSearchResult : class;
}
