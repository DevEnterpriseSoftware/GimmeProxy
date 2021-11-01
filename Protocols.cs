using System;

namespace GimmeProxy
{
  /// <summary>
  /// A bit-field of flags for specifying supported protocols.
  /// </summary>
  [Flags]
  public enum Protocols
  {
    /// <summary>
    /// No protocol flags provided.
    /// </summary>
    None = 0,

    /// <summary>
    /// Proxy uses HTTP.
    /// </summary>
    HTTP = 1,

    /// <summary>
    /// Proxy uses SOCKS4
    /// </summary>
    SOCKS4 = 2,

    /// <summary>
    /// Proxy uses SOCKS5
    /// </summary>
    SOCKS5 = 4,
  }
}
