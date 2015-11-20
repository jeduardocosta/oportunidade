using MinutoSeguros.BlogFeed.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinutoSeguros.BlogFeed.API.Extensions;

namespace MinutoSeguros.BlogFeed.API.Models
{
    public class TopWordsResponse
    {
        [JsonProperty(PropertyName = "metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty(PropertyName = "topWords")]
        public IEnumerable<TopWord> TopWords { get; set; }

        public TopWordsResponse(RequestParameters requestParameters, IEnumerable<BlogFeedContent> blogFeedContent)
        {
            LoadMetadaAttribute(requestParameters);
            LoadTopWordsAttribute(blogFeedContent, requestParameters.Limit, requestParameters.Offset);
        }

        private void LoadMetadaAttribute(RequestParameters requestParameters)
        {
            Metadata = new Metadata(requestParameters.Limit, requestParameters.Offset);
        }

        private void LoadTopWordsAttribute(IEnumerable<BlogFeedContent> blogFeedContent, int limit, int offset)
        {
            var sourceTopWords = GetSourceTopWords(blogFeedContent);
            var filteredTopWords = sourceTopWords.OrderByDescending(it => it.Value).Pagination(limit, offset);

            var topWords = new List<TopWord>();

            foreach (var item in filteredTopWords)
            {
                var topWord = new TopWord(item.Key.ToLower(), item.Value);
                topWords.Add(topWord);
            }

            TopWords = topWords;
        }

        private IDictionary<string, int> GetSourceTopWords(IEnumerable<BlogFeedContent> blogFeedContent)
        {
            var allTopWords = blogFeedContent.SelectMany(it => it.TopWords).Distinct();
            var allWords = allTopWords.Select(it => it.Key).Distinct();

            var topWords = new ConcurrentDictionary<string, int>();

            Parallel.ForEach(allWords, word =>
            {
                var wordOccurrences = GetKeyOccurrencesFromKeyValuePairStructure(allTopWords, word);
                topWords.TryAdd(word, wordOccurrences);
            });

            return new Dictionary<string, int>(topWords);
        }

        private int GetKeyOccurrencesFromKeyValuePairStructure(IEnumerable<KeyValuePair<string, int>> source, string key)
        {
            return source
                .Where(it => string.Equals(it.Key, key, StringComparison.OrdinalIgnoreCase))
                .Sum(it => it.Value);
        }
    }
}