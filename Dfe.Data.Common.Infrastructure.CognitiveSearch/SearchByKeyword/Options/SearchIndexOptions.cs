namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword.Options;

/// <summary>
/// Configuration options used to define the search index .
/// </summary>
public sealed class SearchIndexOptions
{
    /// <summary>
    /// The name of the index used to perform searching over.
    /// </summary>
    public string? IndexName { get; set; }
}
