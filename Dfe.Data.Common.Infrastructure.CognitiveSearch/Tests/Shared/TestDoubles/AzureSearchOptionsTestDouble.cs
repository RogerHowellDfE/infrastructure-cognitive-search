using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles;

internal static class AzureSearchOptionsTestDouble
{
    public static SearchOptions SearchOptionsBasic() => new()
    {
        SearchMode = SearchMode.Any,
        Size = 100,
        IncludeTotalCount = true,
        SearchFields = { "searchField1" }
    };

    public static SearchOptions SearchOptionsWithSearchField(string searchField) => new()
    {
        SearchMode = SearchMode.Any,
        Size = 100,
        IncludeTotalCount = true,
        SearchFields = { searchField }
    };
}
