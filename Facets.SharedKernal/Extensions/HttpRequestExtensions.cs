using Microsoft.AspNetCore.Http;

namespace Facets.SharedKernal.Extensions;

public static class HttpRequestExtensions
{
    public static string BaseUrl(this HttpRequest req)
    {
        if (req == null) return string.Empty;
        var uriBuilder = new UriBuilder(req.Scheme, req.Host.Host, req.Host.Port ?? -1);
        if (uriBuilder.Uri.IsDefaultPort)
        {
            uriBuilder.Port = -1;
        }

        return uriBuilder.Uri.AbsoluteUri;
    }
}
