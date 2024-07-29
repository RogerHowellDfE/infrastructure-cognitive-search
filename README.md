
# infrastructure-cognitive-search
A library to provide an accessible API for working with Azure cognitive search. The package contains a fully configured default service for searching by keyword, as well as a geo-location service which allows searches to be made by town, or post-code. The package is intended to take the heavy-lifting away in terms of setup and configurartion and allow for an easy, pluggable set of components that can be used across projects. 


## Getting Started
In order to leverage the search functionality it is necessary to register a number of default dependencies. The dependencies can be used in isolation or registered as a whole under a single composition root, as below.

### Prerequisites

In order to use the default search services it is possible to register all dependcies listed under the default composition root, in one registration, as follows:

```
builder.Services.RegisterDefaultSearchServices(builder.Configuration);
```

Alternatively, the registrations can be configured in the consuming application IOC container, with a typical registration configured as follows:

```
builder.Services.AddAzureCognitiveSearchProvider(builder.Configuration);
builder.Services.AddScoped(typeof(ISearchServiceAdapter), typeof(CognitiveSearchServiceAdapter<Infrastructure.Establishment>));
builder.Services.AddScoped<IUseCase<SearchByKeywordRequest, SearchByKeywordResponse>, SearchByKeywordUseCase>();
builder.Services.AddSingleton(typeof(IMapper<Response<SearchResults<Infrastructure.Establishment>>, EstablishmentResults>), typeof(AzureSearchResponseToEstablishmentResultMapper));
builder.Services.AddSingleton<IMapper<SearchSettingsOptions, SearchOptions>, SearchOptionsToAzureOptionsMapper>();
builder.Services.AddSingleton<IMapper<SearchByKeywordResponse, SearchResultsViewModel>, SearchByKeywordResponseToViewModelMapper>();
builder.Services.AddSingleton<IMapper<Infrastructure.Establishment, Search.Establishment>, AzureSearchResultToEstablishmentMapper>();
builder.Services.AddSingleton<IMapper<EstablishmentResults, SearchByKeywordResponse>, ResultsToResponseMapper>();

builder.Services.AddOptions<SearchSettingsOptions>("establishments")
    .Configure<IConfiguration>(
        (settings, configuration) =>
            configuration.GetSection("AzureCognitiveSearchOptions:SearchEstablishment:SearchSettingsOptions").Bind(settings));

builder.Services.AddScoped<ISearchOptionsFactory, SearchOptionsFactory>();
```
### Code Usage/Examples

Typical dependency injection and search request would look something like the following,

```
public class HomeController : Controller
{
    private readonly IUseCase<SearchByKeywordRequest, SearchByKeywordResponse> _searchByKeywordUseCase;
    private readonly IMapper<SearchByKeywordResponse, SearchResultsViewModel> _mapper;

    public HomeController(
        ILogger<HomeController> logger,
        IUseCase<SearchByKeywordRequest, SearchByKeywordResponse> searchByKeywordUseCase,
        IMapper<SearchByKeywordResponse, SearchResultsViewModel> mapper)
    {
        _searchByKeywordUseCase = searchByKeywordUseCase;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(string searchKeyWord)
    {
        if (string.IsNullOrEmpty(searchKeyWord))
        {
            return View();
        }
        ViewBag.SearchQuery = searchKeyWord;

        SearchByKeywordResponse response =
            await _searchByKeywordUseCase.HandleRequest(
                new SearchByKeywordRequest(searchKeyWord, "establishments"));

        SearchResultsViewModel viewModel = _mapper.MapFrom(response);
        return View(viewModel);
    }
}
```

## Built With

* [.Net 8](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview) - Core framework used
* [Azure](https://learn.microsoft.com/en-us/azure/search/) - Cloud services provider (cognitive search)


## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Spencer O'Hegarty** - *Initial work*
* **Catherine Lawlor**
* **Asia Witek**

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details


