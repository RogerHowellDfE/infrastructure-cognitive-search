using Microsoft.Extensions.Options;
using Moq;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles;

internal static class IOptionsTestDouble
{
    public static Mock<IOptions<TOptionSetting>> IOptionsMock<TOptionSetting>()
        where TOptionSetting : class => new();

    public static IOptions<TOptionsSetting> IOptionsMockFor<TOptionsSetting>(TOptionsSetting optionsSettings)
        where TOptionsSetting : class
    {
        var optionsMock = IOptionsMock<TOptionsSetting>();

        optionsMock.Setup(options =>
            options.Value).Returns(optionsSettings);

        return optionsMock.Object;
    }
}
