using MinutoSeguros.BlogFeed.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MinutoSeguros.BlogFeed.API.Parsers
{
    public interface IRequestParametersParser
    {
        RequestParameters Parse(HttpRequestMessage request);
    }

    public class RequestParametersParser : IRequestParametersParser
    {
        private const string feedUrlKeyName = "feedurl";
        private const string limitKeyName = "limit";
        private const string offsetKeyName = "offset";
        private const string numberOfTopWordsKeyName = "numberoftopwords";

        public RequestParameters Parse(HttpRequestMessage request)
        {
            var sourceParameters = ExtractParametersBy(request);

            var feedUrl = GetContentByKeyName(feedUrlKeyName, sourceParameters);
            var limit = GetContentByKeyName(limitKeyName, sourceParameters);
            var offset = GetContentByKeyName(offsetKeyName, sourceParameters);
            var numberOfTopWords = GetContentByKeyName(numberOfTopWordsKeyName, sourceParameters);

            var requestParameters = new RequestParameters(feedUrl, limit, offset, numberOfTopWords);
            return requestParameters;
        }

        private string GetContentByKeyName(string keyName, Dictionary<string, string> sourceParameters)
        {
            var obtained = sourceParameters
                .Where(it => string.Equals(it.Key, keyName, StringComparison.OrdinalIgnoreCase))
                .Select(it => it.Value)
                .FirstOrDefault();

            return obtained;
        }

        private Dictionary<string, string> ExtractParametersBy(HttpRequestMessage request)
        {
            return request
                .GetQueryNameValuePairs()
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}