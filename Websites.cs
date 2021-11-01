using System;

namespace GimmeProxy
{
  /// <summary>
  /// A bit-field of flags for specifying supported websites.
  /// </summary>
  [Flags]
  public enum Websites
  {
    /// <summary>
    /// No website flags provided.
    /// </summary>
    None = 0,

    /// <summary>
    /// Tested working against Example.
    /// </summary>
    Example = 1,

    /// <summary>
    /// Tested working against Google.
    /// </summary>
    Google = 2,

    /// <summary>
    /// Tested working against Amazon.
    /// </summary>
    Amazon = 4,

    /// <summary>
    /// Tested working against Yelp.
    /// </summary>
    Yelp = 8,

    /// <summary>
    /// Tested working against Google Maps.
    /// </summary>
    Google_Maps = 16,
  }
}
