using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GimmeProxy
{
  /// <summary>
  /// A GimmeProxy API client.
  /// </summary>
  public static class GimmeProxyClient
  {
    private static readonly HttpClient defaultHttpClient = new();

    /// <summary>
    /// Returns one random proxy with no specific options.
    /// </summary>
    /// <exception cref="HttpRequestException">Can be thrown if the request was not successful.</exception>
    /// <param name="cancellationToken">(Optional) A token that allows processing to be cancelled.</param>
    /// <returns>
    /// Random proxy details.
    /// </returns>
    public static Task<GimmeProxyResponse> GetRandomProxy(CancellationToken cancellationToken = default)
      => GetRandomProxy(defaultHttpClient, new(), cancellationToken);

    /// <summary>
    /// Returns one random proxy with additional filter parameters.
    /// </summary>
    /// <exception cref="HttpRequestException">Can be thrown if the request was not successful.</exception>
    /// <param name="proxyOptions">Options for filtering proxy result.</param>
    /// <param name="cancellationToken">(Optional) A token that allows processing to be cancelled.</param>
    /// <returns>
    /// Random proxy details.
    /// </returns>
    public static Task<GimmeProxyResponse> GetRandomProxy(GimmeProxyRequest proxyOptions, CancellationToken cancellationToken = default)
      => GetRandomProxy(defaultHttpClient, proxyOptions, cancellationToken);

    /// <summary>
    /// Returns one random proxy with additional filter parameters.
    /// </summary>
    /// <exception cref="HttpRequestException">Can be thrown if the request was not successful.</exception>
    /// <param name="httpClient">Provide your own HTTP client to make the request.</param>
    /// <param name="proxyOptions">Options for filtering proxy result.</param>
    /// <param name="cancellationToken">(Optional) A token that allows processing to be cancelled.</param>
    /// <returns>
    /// Random proxy details.
    /// </returns>
    public static async Task<GimmeProxyResponse> GetRandomProxy(HttpClient httpClient, GimmeProxyRequest proxyOptions, CancellationToken cancellationToken = default)
    {
      var response = await httpClient.GetAsync(proxyOptions.ToString(), cancellationToken).ConfigureAwait(false);

      response.EnsureSuccessStatusCode();
#if NET472
      var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#else
      var json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#endif

#if NET472
      return JsonConvert.DeserializeObject<GimmeProxyResponse>(json.Replace("<br>", string.Empty).Replace("<BR>", string.Empty));
#else
      // Some data comes back with <br> tags, this is just a sweeping replace cleanup.
      return JsonConvert.DeserializeObject<GimmeProxyResponse>(json.Replace("<br>", string.Empty, StringComparison.OrdinalIgnoreCase));
#endif
    }
  }
}
