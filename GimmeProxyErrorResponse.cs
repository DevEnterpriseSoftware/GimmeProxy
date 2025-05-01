using System.Runtime.Serialization;

namespace GimmeProxy;

/// <summary>
/// A lookup error response.
/// </summary>
[DataContract]
public class GimmeProxyErrorResponse
{
  /// <summary>
  /// The error message.
  /// </summary>
  [DataMember(Name = "error")]
  public string? ErrorMessage { get; init; }
}
