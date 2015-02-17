using HtmlAgilityPack;
using MinutoSeguros.BlogFeed.Core.Exceptions;
using MinutoSeguros.BlogFeed.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MinutoSeguros.BlogFeed.Core.Helpers
{
    public interface IHtmlHelper
    {
        string RemoveTags(string source);
    }

    public class HtmlHelper : IHtmlHelper
    {
        private ILogger _logger;

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

                _logger.Error(string.Format("{0} - value: {1}.", errorMessage, htmlContent), exception);
                throw new CustomErrorException(errorMessage);
            }
        }

        private string Encode(string source)
        {
            return HttpUtility.HtmlDecode(source);
        }
    }
}