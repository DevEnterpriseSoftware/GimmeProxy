# GimmeProxy

[GimmeProxy](https://gimmeproxy.com/) constantly crawls the internet for working proxies, so you don't have to.

This is a C# wrapper around the [GimmeProxy API](https://gimmeproxy.com/#api).

### Getting started

Install GimmeProxy from NuGet:

```
Install-Package GimmeProxy
```

Random proxy getter methods are provided in a simple static class called `GimmeProxyClient`.
Use the various `GetRandomProxyAsync` overloads to retrive a random proxy using the [GimmeProxy API]([GimmeProxy - free rotating proxy api](https://gimmeproxy.com/#api)).

```csharp
using GimmeProxy;

public class Program
{
  static async Task MainAsync(string[] args)
  {
    // Get a random proxy with no filtering.
    var randomProxy = await GimmeProxyClient.GetRandomProxyAsync();

    // Returns only proxies that support GET requests, HTTPS and were checked in last 3600 seconds.
    var requestOptions = new GimmeProxyRequest
    {
      SupportsPost = true,
      SupportsHttps = true,
      CheckedSecondsAgo = 3600,
    };

    // https://gimmeproxy.com/api/getProxy?post=true&supportsHttps=true&maxCheckPeriod=3600
    var url = request.ToString();
    
    // You can also sign-up for an API key.
    requestOptions.ApiKey = "xxxxxxx";
    
    var anotherProxy = await GimmeProxyClient.GetRandomProxyAsync(requestOptions);
  }
}
```

The wrapper is fully documented. If you need more information, please refer to the [GimmeProxy API page](https://gimmeproxy.com/#api).
