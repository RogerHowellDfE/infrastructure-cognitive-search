using Azure;
using Azure.Search.Documents.Models;
using Moq;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.SearchByKeyword.TestDoubles;

internal static class SearchResultsTestDouble<T>
{
    public static SearchResults<T> SearchResultsWith(T searchResultDocument) =>
        SearchModelFactory.SearchResults(
            new[]
            {
                SearchResultWith(searchResultDocument)
            },
            20,
            null,
            null,
            new Mock<Response>().Object
        );

    public static SearchResult<T> SearchResultWith(T searchResultDocument) =>
        SearchModelFactory.SearchResult(searchResultDocument, 0.9, null);
}
