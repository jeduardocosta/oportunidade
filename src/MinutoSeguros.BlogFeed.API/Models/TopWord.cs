using Newtonsoft.Json;

namespace MinutoSeguros.BlogFeed.API.Models
{
    public class TopWord
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "occurrences")]
        public int Occurrences { get; set; }

        public TopWord(string name, int occureences)
        {
            Name = name;
            Occurrences = occureences;
        }

        public bool Equals(TopWord other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Name == Name && other.Occurrences == Occurrences;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(TopWord)) return false;
            return Equals((TopWord)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name?.GetHashCode() ?? 0) ^ (Occurrences.GetHashCode() * 397);
            }
        }
    }
}