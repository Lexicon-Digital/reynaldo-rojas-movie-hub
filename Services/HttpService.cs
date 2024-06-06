using Polly;
using Polly.Extensions.Http;

namespace MoviesAPI;

public class HttpService
{
  private HttpClient _client;

  private IAsyncPolicy<HttpResponseMessage> _retryPolicy;

  const int retry = 3;

  public HttpService(IConfiguration configuration)
  {
    _client = new HttpClient();
    _client.BaseAddress = new Uri(configuration["MovieProviders:PrincessTheatre:API"]);
    _client.DefaultRequestHeaders.Add(
      configuration["MovieProviders:PrincessTheatre:ApiKey:Header"],
      configuration["MovieProviders:PrincessTheatre:ApiKey:Token"]
    );

    _retryPolicy = HttpPolicyExtensions
      .HandleTransientHttpError()
      .WaitAndRetryAsync(retry, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
  }

  public async Task<HttpResponseMessage> GetRetryAsync(string url)
  {
    return await _retryPolicy.ExecuteAsync(() => _client.GetAsync(url));
  }
}
