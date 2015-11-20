using HtmlAgilityPack;
using MinutoSeguros.BlogFeed.Core.Exceptions;
using MinutoSeguros.BlogFeed.Log;
using System;
using System.Web;

namespace MinutoSeguros.BlogFeed.Core.Helpers
{
    public class HtmlHelper : IHtmlHelper
    {
        private readonly ILogger _logger;

        public HtmlHelper(ILogger logger)
        {
            _logger = logger;
        }

        public string RemoveTags(string htmlContent)
        {
            try
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                var htmlInnerText = htmlDocument.DocumentNode.InnerText;

                var uncodedContent = Encode(htmlInnerText);
                return uncodedContent;
            }
            catch (Exception exception)
            {
                const string errorMessage = "failed to remove tags from html content.";

                _logger.Error($"{errorMessage} - value: {htmlContent}.", exception);
                throw new CustomErrorException(errorMessage);
            }
        }

        private static string Encode(string source)
        {
            return HttpUtility.HtmlDecode(source);
        }
    }
}