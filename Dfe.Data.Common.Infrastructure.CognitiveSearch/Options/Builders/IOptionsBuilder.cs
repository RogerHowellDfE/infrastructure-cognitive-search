namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Options.Builders;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TOptions">
/// 
/// </typeparam>
public interface IOptionsBuilder<TOptions>
{
    /// <summary>
    /// 
    /// </summary>
    string? Filter { get; }
    
    /// <summary>
    /// 
    /// </summary>
    string? HighlightPostTag { get; }
    
    /// <summary>
    /// 
    /// </summary>
    string? HighlightPreTag { get; }

    /// <summary>
    /// 
    /// </summary>
    double? MinimumCoverage { get; }

    /// <summary>
    /// 
    /// </summary>
    int? Size { get; }

    /// <summary>
    /// 
    /// </summary>
    IList<string>? SearchFields { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    IOptionsBuilder<TOptions> WithResultSize(int size);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    TOptions Build();
}