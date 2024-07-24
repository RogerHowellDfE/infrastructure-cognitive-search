namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Options.Builders;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TOptions"></typeparam>
public abstract class DefaultOptionsBuilder<TOptions> : IOptionsBuilder<TOptions>
{
    private int _resultSize;

    /// <summary>
    /// 
    /// </summary>
    public string? Filter { get; }

    /// <summary>
    /// 
    /// </summary>
    public string? HighlightPostTag { get; }

    /// <summary>
    /// 
    /// </summary>
    public string? HighlightPreTag { get; }

    /// <summary>
    /// 
    /// </summary>
    public double? MinimumCoverage { get; }

    /// <summary>
    /// 
    /// </summary>
    public int? Size => _resultSize;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public IOptionsBuilder<TOptions> WithResultSize(int size){
        _resultSize = size;
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public abstract TOptions Build();

    /// <summary>
    /// 
    /// </summary>
    public IList<string> SearchFields { get; } = [];
}
