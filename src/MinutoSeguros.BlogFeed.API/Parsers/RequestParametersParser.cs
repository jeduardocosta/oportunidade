using MinutoSeguros.BlogFeed.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MinutoSeguros.BlogFeed.API.Parsers
{
    public interface IRequestParametersParser
    {
        RequestParameters Parse(HttpRequestMessage request);
    }

    public class RequestParametersParser : IRequestParametersParser
    {
        private const string FeedUrlKeyName = "feedurl";
        private const string LimitKeyName = "limit";
        private const string OffsetKeyName = "offset";
        private const string NumberOfTopWordsKeyName = "numberoftopwords";

        public RequestParameters Parse(HttpRequestMessage request)
        {
            var sourceParameters = ExtractParametersBy(request);

            var feedUrl = GetContentByKeyName(FeedUrlKeyName, sourceParameters);
            var limit = GetContentByKeyName(LimitKeyName, sourceParameters);
            var offset = GetContentByKeyName(OffsetKeyName, sourceParameters);
            var numberOfTopWords = GetContentByKeyName(NumberOfTopWordsKeyName, sourceParameters);

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