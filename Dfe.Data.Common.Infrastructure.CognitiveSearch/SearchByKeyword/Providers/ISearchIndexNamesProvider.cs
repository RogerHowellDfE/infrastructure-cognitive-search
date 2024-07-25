namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.SearchByKeyword.Providers;

/// <summary>
/// 
/// </summary>
public interface ISearchIndexNamesProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetIndexNames();
}
