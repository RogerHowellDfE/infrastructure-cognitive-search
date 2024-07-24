namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Options.Builders;

/// <summary>
/// 
/// </summary>
public static class OptionsEnumerator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="enumerableOptions"></param>
    /// <param name="enumerableOptionAction"></param>
    public static void EnumerateOptions(
        this IEnumerable<string> enumerableOptions,
        Action<string> enumerableOptionAction)
    {
        enumerableOptions?.ToList()
                .ForEach(enumerableOption =>
                    enumerableOptionAction(enumerableOption));
    }
}
