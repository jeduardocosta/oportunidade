namespace MinutoSeguros.BlogFeed.Core.Helpers
{
    public interface IUrlHelper
    {
        bool IsValidUrl(string url);

        bool IsValidAbsoluteUrl(string url);
    }
}