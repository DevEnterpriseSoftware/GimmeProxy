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
    public static Task<GimmeProxyResponse> GetRandomProxyAsync(CancellationToken cancellationToken = default)
      => GetRandomProxyAsync(defaultHttpClient, new(), cancellationToken);

    /// <summary>
    /// Returns one random proxy with additional filter parameters.
    /// </summary>
    /// <exception cref="HttpRequestException">Can be thrown if the request was not successful.</exception>
    /// <param name="proxyOptions">Options for filtering proxy result.</param>
    /// <param name="cancellationToken">(Optional) A token that allows processing to be cancelled.</param>
    /// <returns>
    /// Random proxy details.
    /// </returns>
    public static Task<GimmeProxyResponse> GetRandomProxyAsync(GimmeProxyRequest proxyOptions, CancellationToken cancellationToken = default)
      => GetRandomProxyAsync(defaultHttpClient, proxyOptions, cancellationToken);

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
    public static async Task<GimmeProxyResponse> GetRandomProxyAsync(HttpClient httpClient, GimmeProxyRequest proxyOptions, CancellationToken cancellationToken = default)
    {
      var url = proxyOptions.ToString();
      var response = await httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);

      response.EnsureSuccessStatusCode();

#if NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET45 || NET451 || NET452 || NET6 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
      var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

      var cleanJson = json.Replace("<br>", string.Empty)
                          .Replace("<BR>", string.Empty)
                          .Replace("{},", "[],");
#else
      var json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

      // Some data comes back with <br> tags, this is just a sweeping replace cleanup.
      // OtherProtocols also comes back as an object instead of an empty array.
      var cleanJson = json.Replace("<br>", string.Empty, StringComparison.OrdinalIgnoreCase)
                          .Replace("{},", "[],");
#endif

      return JsonConvert.DeserializeObject<GimmeProxyResponse>(cleanJson);
    }
  }
}
