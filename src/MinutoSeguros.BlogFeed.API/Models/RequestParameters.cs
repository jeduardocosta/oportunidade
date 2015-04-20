using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinutoSeguros.BlogFeed.API.Models
{
    public class RequestParameters
    {
        private int _limit;
        private int _offset;

        private const int LimitDefaultValue = 10;
        private const int LimitMaximumValue = 10;
        private const int OffsetDefaultValue = 0;
        private const int NumberOfTopWordsDefaultValue = 10;

        public string FeedUrl { get; private set; }

        public int NumberOfTopWords { get; private set; }

        public int Limit 
        { 
            get  { return _limit; }
            private set { _limit = IsValidLimitValue(value) ? value : LimitMaximumValue; } 
        }

        public int Offset
        {
            get { return _offset; }
            private set { _offset = IsValidOffsetValue(value) ? value : 0; } 
        }

        public RequestParameters(string feedUrl, string limit, string offset, string numberOfTopWords)
        {
            FeedUrl = feedUrl;
            Limit = GetValueOrDefault(limit, LimitDefaultValue);
            Offset = GetValueOrDefault(offset, OffsetDefaultValue);
            NumberOfTopWords = GetValueOrDefault(numberOfTopWords, NumberOfTopWordsDefaultValue);
        }

        private int GetValueOrDefault(string value, int defaultValue)
        {
            return string.IsNullOrEmpty(value) ? defaultValue : int.Parse(value);
        }

        private bool IsValidLimitValue(int value)
        {
            return value <= LimitMaximumValue;
        }

        private bool IsValidOffsetValue(int value)
        {
            return value >= 0;
        }
    }
}