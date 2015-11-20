using Newtonsoft.Json;

namespace MinutoSeguros.BlogFeed.API.Models
{
    public class Metadata
    {
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        public Metadata(int limit, int offset)
        {
            Limit = limit;
            Offset = offset;
        }
    }
}