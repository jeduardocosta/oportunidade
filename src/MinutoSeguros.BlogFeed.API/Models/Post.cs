using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinutoSeguros.BlogFeed.API.Models
{
    public class Post
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "publishDate")]
        public DateTime PublishDate { get; set; }

        [JsonProperty(PropertyName = "categories")]
        public IEnumerable<string> Categories { get; set; }

        [JsonProperty(PropertyName = "totalWords")]
        public int TotalWords { get; set; }

        [JsonProperty(PropertyName = "topWords")]
        public IEnumerable<TopWord> TopWords { get; set; }

        public Post(string title, DateTime publishDate, IEnumerable<string> categories, Dictionary<string, int> sourceTopWords, int numberOfTopWords)
        {
            Title = title;
            PublishDate = publishDate;
            Categories = categories;

            LoadTotalWordsAttribute(sourceTopWords);
            LoadTopWordsAttribute(sourceTopWords, numberOfTopWords);
        }

        private void LoadTotalWordsAttribute(Dictionary<string, int> sourceTopWords)
        {
            TotalWords = sourceTopWords.Distinct().Count();
        }

        private void LoadTopWordsAttribute(Dictionary<string, int> sourceTopWords, int numberOfTopWords)
        {
            var filteredSourceTopWords = sourceTopWords
                .OrderByDescending(it => it.Value)
                .Take(numberOfTopWords);

            var topWords = new List<TopWord>();

            foreach (var item in filteredSourceTopWords)
            {
                var topWord = new TopWord(item.Key.ToLower(), item.Value);
                topWords.Add(topWord);
            }

            TopWords = topWords;
        }
    }
}