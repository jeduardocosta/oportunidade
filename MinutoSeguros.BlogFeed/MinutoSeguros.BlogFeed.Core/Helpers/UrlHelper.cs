using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.Core.Helpers
{
    public interface IUrlHelper
    {
        bool IsValidUrl(string url);

        bool IsValidAbsoluteUrl(string url);
    }

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