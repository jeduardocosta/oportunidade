﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MinutoSeguros.BlogFeed.Core.Entities
{
    public class BlogFeedContent
    {
        public string Title { get; private set; }
        public DateTime PublishDate { get; private set; }
        public IEnumerable<string> Categories { get; private set; }
        public string FullContent { get; }

        public Dictionary<string, int> TopWords { get; private set; }

        public BlogFeedContent(string title, DateTime publishDate, IEnumerable<string> categories, string content)
        {
            Title = title;
            PublishDate = publishDate;
            Categories = categories;
            FullContent = content;

            LoadTopWordsAttribute(FullContent);
        }

        private void LoadTopWordsAttribute(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            { 
                return;
            }

            var filteredContent = RemovedUnnecessaryWords(source);
            TopWords = GetTopWords(filteredContent);
        }

        private string RemovedUnnecessaryWords(string source)
        {
            var filteredContent = source;
            filteredContent = RemoveArticles(filteredContent);
            filteredContent = RemovePrepositions(filteredContent);
            return filteredContent;
        }

        internal static string RemoveArticles(string source)
        {
            const string pattern = @"\b(o|a|os|as|um|uma|uns|umas)\b";
            var filtered = Regex.Replace(source, pattern, string.Empty, RegexOptions.IgnoreCase);
            return filtered.Trim();
        }

        internal static string RemovePrepositions(string source)
        {
            const string pattern = @"\b(a|ante|após|até|com|contra|de|desde|em|entre|para|por|perante|segundo|sem|sob|sobre|trás|afora|fora|exceto|salvo|malgrado|durante|mediante|segundo|menos)\b";
            var filtered = Regex.Replace(source, pattern, string.Empty, RegexOptions.IgnoreCase);
            return filtered.Trim();
        }

        private Dictionary<string, int> GetTopWords(string source)
        {
            return Regex.Split(source.ToLower(), @"\W+")
                .Where(s => s.Length > 3)
                .GroupBy(s => s)
                .OrderByDescending(g => g.Count())
                .ToDictionary(k => k.Key, v => v.Count());
        }
    }
}