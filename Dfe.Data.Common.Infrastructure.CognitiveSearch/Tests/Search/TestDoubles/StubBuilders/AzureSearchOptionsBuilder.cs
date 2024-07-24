using Dfe.Data.Common.Infrastructure.CognitiveSearch.Options;

namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.Search.TestDoubles.StubBuilders;

internal class AzureSearchOptionsBuilder
{
    private string _credentials = "fhjk67=j#l";
    private string _endpointUri = Faker.Internet.SecureUrl();

    public AzureSearchClientOptions Create() =>
        new AzureSearchClientOptions()
        {
            Credentials = _credentials,
            EndpointUri = _endpointUri,
        };

    public AzureSearchOptionsBuilder WithInvalidCredentials()
    {
        _credentials = "";
        return this;
    }
    public AzureSearchOptionsBuilder WithInvalidEndpoint()
    {
        _endpointUri = "";
        return this;
    }
}
