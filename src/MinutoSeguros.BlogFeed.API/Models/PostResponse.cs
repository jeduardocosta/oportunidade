using MinutoSeguros.BlogFeed.API.Extensions;
using MinutoSeguros.BlogFeed.Core.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MinutoSeguros.BlogFeed.API.Models
{
    public class PostResponse
    {
        [JsonProperty(PropertyName = "metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty(PropertyName = "posts")]
        public IEnumerable<Post> Posts { get; set; }

        public PostResponse(RequestParameters requestParameters, IEnumerable<BlogFeedContent> blogFeedContent)
        {
            LoadMetadaAttribute(requestParameters);
            LoadPostsAttribute(blogFeedContent, requestParameters.Limit, requestParameters.Offset, requestParameters.NumberOfTopWords);
        }

        private void LoadMetadaAttribute(RequestParameters requestParameters)
        {
            Metadata = new Metadata(requestParameters.Limit, requestParameters.Offset);
        }

        private void LoadPostsAttribute(IEnumerable<BlogFeedContent> blogFeedContent, int limit, int offset, int numberOfTopWords)
        {
            var posts = new List<Post>();

            var filteredBlogFeedContent = blogFeedContent.Pagination(limit, offset);

            foreach (var item in filteredBlogFeedContent)
            {
                var post = new Post(item.Title, item.PublishDate, item.Categories, item.TopWords, numberOfTopWords);
                posts.Add(post);
            }

            Posts = posts;
        }
    }
}