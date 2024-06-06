using System.Reflection;

namespace MoviesAPI;

public class PrincessTheatreService
{
  HttpService _httpService;

  public PrincessTheatreService(HttpService httpService)
  {
    _httpService = httpService;
  }

  public async Task<ProviderDto?> GetMoviesByProvider(string provider)
  {
    HttpResponseMessage response = await _httpService.GetRetryAsync($"{provider}/movies");
    return await ProcessResponse(response);
  }

  private async Task<ProviderDto?> ProcessResponse(HttpResponseMessage responseMessage)
  {
    var movieProvider = new ProviderDto();
    if (responseMessage.IsSuccessStatusCode)
    {
      movieProvider = await responseMessage.Content.ReadFromJsonAsync<ProviderDto>();
    }
    return movieProvider;
  }

  private string GetFormattedId(string provider, string id)
  {
    return provider + id;
  }

  public PrincessTheatreMovieDto? GetMovieByReferenceId(ProviderDto movieProvider, string princessTheatreReferenceId, string provider)
  {
    return movieProvider?.Movies
      .Where(m => m.ID == GetFormattedId(provider, princessTheatreReferenceId))
      .FirstOrDefault();
  }

  public async Task<List<CinemaDto>> GetCinemasByReferenceId(string referenceId)
  {
    List<CinemaDto> princessTheatreCinemas = new List<CinemaDto>();
    Type type = MovieProviders.providers.GetType();
    PropertyInfo[] properties = type.GetProperties();

    foreach (PropertyInfo property in properties)
    {
      // Providers are represented in the class MovieProviders
      object value = property.GetValue(MovieProviders.providers);
      MovieProviderInfo movieProviderInfo = (MovieProviderInfo)value;

      // Fetch movies from provider
      var movieProvider = await GetMoviesByProvider(movieProviderInfo.URLSegment);

      // Search for movie
      var movieFromProvider = GetMovieByReferenceId(
        movieProvider,
        referenceId,
        movieProviderInfo.Short
      );

      if (movieFromProvider != null)
      {
        princessTheatreCinemas.Add(new CinemaDto
        {
          Name = movieProviderInfo.Name,
          TicketPrice = Convert.ToDecimal(movieFromProvider.Price.ToString())
        });
      }
    }
    return princessTheatreCinemas;
  }
}

public class MovieProvider
{
  public MovieProviderInfo Filmworld { get; set; } = new MovieProviderInfo { };
  public MovieProviderInfo Cinemaworld { get; set; } = new MovieProviderInfo { };
}

public class MovieProviderInfo
{
  public string Name { get; set; } = String.Empty;
  public string URLSegment { get; set; } = String.Empty;
  public string Short { get; set; } = String.Empty;
}

public class MovieProviders
{
  public static readonly MovieProvider providers;

  static MovieProviders()
  {
    providers = new MovieProvider
    {
      Filmworld = new MovieProviderInfo
      {
        Name = "Filmworld",
        URLSegment = "filmworld",
        Short = "fw"
      },
      Cinemaworld = new MovieProviderInfo
      {
        Name = "Cinemaworld",
        URLSegment = "cinemaworld",
        Short = "cw"
      },
    };
  }
}