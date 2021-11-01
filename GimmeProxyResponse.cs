using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GimmeProxy
{
  /// <summary>
  /// One random working proxy from our database.
  /// </summary>
  [DataContract]
  public class GimmeProxyResponse : ResponseProxyInformation
  {
    /// <summary>
    /// The last time this proxy was checked (UNIX timestamp).
    /// </summary>
    /// <seealso cref="LastChecked"/>
    [DataMember(Name = "tsChecked")]
    public long LastCheckedTimestamp { get; set; }

    /// <summary>
    /// The last time this proxy was checked.
    /// </summary>
    public TimeSpan LastChecked => DateTimeOffset.UtcNow - DateTimeOffset.FromUnixTimeSeconds(LastCheckedTimestamp);

    /// <summary>
    /// The speed of the proxy in kilobytes.
    /// </summary>
    /// <value>
    /// The speed.
    /// </value>
    [DataMember(Name = "speed")]
    public float SpeedInKilobytes { get; set; }

    /// <summary>
    /// Other protocols supported by this proxy (if any).
    /// </summary>
    [DataMember(Name = "otherProtocols")]
    public List<ResponseProxyInformation> OtherProtocols { get; } = new List<ResponseProxyInformation>();
  }
}
