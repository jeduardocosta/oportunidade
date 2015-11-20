using System;

namespace MinutoSeguros.BlogFeed.Core.Helpers
{
    public class UrlHelper : IUrlHelper
    {
        public bool IsValidUrl(string url)
        {
            var isUri = Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
            return isUri;
        }

        public bool IsValidAbsoluteUrl(string url)
        {
            var isUri = Uri.IsWellFormedUriString(url, UriKind.Absolute);
            return isUri;
        }
    }
}