using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GimmeProxy
{
  /// <summary>
  /// Shared proxy information provided in responses packets.
  /// </summary>
  [DataContract]
  public class ResponseProxyInformation
  {
    /// <summary>
    /// Supports GET requests.
    /// </summary>
    [DataMember(Name = "get")]
    public bool SupportsGet { get; set; }

    /// <summary>
    /// Supports POST requests.
    /// </summary>
    [DataMember(Name = "post")]
    public bool SupportsPost { get; set; }

    /// <summary>
    /// Supports cookies.
    /// </summary>
    [DataMember(Name = "cookies")]
    public bool SupportsCookies { get; set; }

    /// <summary>
    /// Supports the 'referer' header.
    /// </summary>
    [DataMember(Name = "referer")]
    public bool SupportsReferer { get; set; }

    /// <summary>
    /// Supports the 'user-agent' header.
    /// </summary>
    [DataMember(Name = "user-agent")]
    public bool SupportsUserAgent { get; set; }

    /// <summary>
    /// Supports HTTPS.
    /// </summary>
    [DataMember(Name = "supportsHttps")]
    public bool SupportsHttps { get; set; }

    /// <summary>
    /// Anonymity level.
    /// 1 - Anonymous
    /// 0 - Not Anonymous
    /// <see cref="IsAnonymous"/>
    /// </summary>
    [DataMember(Name = "anonymityLevel")]
    public int AnonymityLevel { get; set; }

    /// <summary>
    /// Indicates whether this proxy is anonymous.
    /// </summary>
    public bool IsAnonymous => AnonymityLevel == 1;

    /// <summary>
    /// Supported proxy protocol.
    /// </summary>
    [DataMember(Name = "protocol")]
    public Protocols Protocol { get; set; }

    /// <summary>
    /// The proxy IP address.
    /// </summary>
    [DataMember(Name = "ip")]
    public string IpAddress { get; set; }

    /// <summary>
    /// The proxy port number.
    /// </summary>
    [DataMember(Name = "port")]
    public int Port { get; set; }

    /// <summary>
    /// List of websites that work (and don't work) through this proxy.
    /// </summary>
    [DataMember(Name = "websites")]
    public Dictionary<Websites, bool> Websites { get; set; } = new Dictionary<Websites, bool>();

    /// <summary>
    /// Country this proxy is coming from.
    /// </summary>
    [DataMember(Name = "country")]
    public string Country { get; set; }

    /// <summary>
    /// Combination of the <see cref="IpAddress"/> and the <see cref="Port"/>.
    /// </summary>
    public string IpAddressAndPort => $"{IpAddress}:{Port}";

    /// <summary>
    /// Ready to use CURLOPT_PROXY value.
    /// </summary>
    public string Curl => $"{(SupportsHttps ? "https" : "http")}://{IpAddressAndPort}";
  }
}
