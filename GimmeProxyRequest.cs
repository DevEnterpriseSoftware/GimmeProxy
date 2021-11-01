using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GimmeProxy
{
  /// <summary>
  /// Defines the filtering parameters when requesting a random proxy.
  /// </summary>
  [DataContract]
  public class GimmeProxyRequest
  {
    /// <summary>
    /// Optional API key, allows to scrape faster and enables the <see cref="Websites"/> option.
    /// </summary>
    /// <remarks>
    /// https://gimmeproxy.com/#pricing
    /// </remarks>
    public string ApiKey { get; set; }

    /// <summary>
    /// Return only proxies that support GET requests.
    /// </summary>
    public bool? SupportsGet { get; set; }

    /// <summary>
    /// Return only proxies that support POST requests.
    /// </summary>
    public bool? SupportsPost { get; set; }

    /// <summary>
    /// Return only proxies that support cookies.
    /// </summary>
    public bool? SupportsCookies { get; set; }

    /// <summary>
    /// Return only proxies that support the 'referer' header.
    /// </summary>
    public bool? SupportsReferer { get; set; }

    /// <summary>
    /// Return only proxies that support the 'user-agent' header.
    /// </summary>
    public bool? SupportsUserAgent { get; set; }

    /// <summary>
    /// Return only proxies that support HTTPS.
    /// </summary>
    public bool? SupportsHttps { get; set; }

    /// <summary>
    /// Return only anonymous proxies.
    /// </summary>
    public bool? AnonymousOnly { get; set; }

    /// <summary>
    /// Return only proxies with supported protocols.
    /// </summary>
    public Protocols Protocols { get; set; }

    /// <summary>
    /// Return only proxies with supported ports.
    /// </summary>
    public ICollection<int> Ports { get; } = new HashSet<int>();

    /// <summary>
    /// Return only proxies with specified countries.
    /// </summary>
    /// <remarks>
    /// https://www.nationsonline.org/oneworld/country_code_list.htm
    /// </remarks>
    public ICollection<string> IncludeCountries { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Exclude proxies with specified countries.
    /// </summary>
    /// <remarks>
    /// https://www.nationsonline.org/oneworld/country_code_list.htm
    /// </remarks>
    public ICollection<string> ExcludeCountries { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Return only proxies checked in last number of seconds.
    /// </summary>
    public int? CheckedSecondsAgo { get; set; }

    /// <summary>
    /// Return only proxies allowed by particular websites.
    /// </summary>
    /// <remarks>
    /// Requires an API key.
    /// </remarks>
    public Websites Websites { get; set; }

    /// <summary>
    /// Return only proxies with speed more than specified maximum.
    /// </summary>
    public float? MinimumSpeedInKilobytes { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
      // https://gimmeproxy.com/#api
      var parameters = new List<string>();

      if (!string.IsNullOrEmpty(ApiKey))
      {
        parameters.Add($"api_key={ApiKey}");
      }

      if (SupportsGet.HasValue)
      {
        parameters.Add($"get={SupportsGet.Value.ToString().ToLowerInvariant()}");
      }

      if (SupportsPost.HasValue)
      {
        parameters.Add($"post={SupportsPost.Value.ToString().ToLowerInvariant()}");
      }

      if (SupportsCookies.HasValue)
      {
        parameters.Add($"cookies={SupportsCookies.Value.ToString().ToLowerInvariant()}");
      }

      if (SupportsReferer.HasValue)
      {
        parameters.Add($"referer={SupportsReferer.Value.ToString().ToLowerInvariant()}");
      }

      if (SupportsUserAgent.HasValue)
      {
        parameters.Add($"user-agent={SupportsUserAgent.Value.ToString().ToLowerInvariant()}");
      }

      if (SupportsHttps.HasValue)
      {
        parameters.Add($"supportsHttps={SupportsHttps.Value.ToString().ToLowerInvariant()}");
      }

      if (AnonymousOnly.HasValue)
      {
        parameters.Add($"anonymityLevel={(AnonymousOnly.Value ? "1" : "0")}");
      }

      if (Protocols != Protocols.None)
      {
        parameters.Add($"protocol={Protocols.ToString().ToLowerInvariant().Replace(" ", string.Empty)}");
      }

      if (Ports.Count > 0)
      {
        parameters.Add($"port={string.Join(",", Ports)}");
      }

      if (IncludeCountries.Count > 0)
      {
        parameters.Add($"country={string.Join(",", IncludeCountries).ToUpperInvariant()}");
      }

      if (ExcludeCountries.Count > 0)
      {
        parameters.Add($"notCountry={string.Join(",", ExcludeCountries).ToUpperInvariant()}");
      }

      if (CheckedSecondsAgo.HasValue)
      {
        parameters.Add($"maxCheckPeriod={CheckedSecondsAgo.Value.ToString().ToLowerInvariant()}");
      }

      if (Websites != Websites.None)
      {
        parameters.Add($"websites={Websites.ToString().ToLowerInvariant().Replace(" ", string.Empty)}");
      }

      if (MinimumSpeedInKilobytes.HasValue)
      {
        parameters.Add($"minSpeed={MinimumSpeedInKilobytes.Value}");
      }

      return ("https://gimmeproxy.com/api/getProxy?" + string.Join("&", parameters)).TrimEnd('?');
    }
  }
}
