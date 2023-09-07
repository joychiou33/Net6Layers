using System.Text.Json.Serialization;

namespace ExamLayer.Models
{
    public class GetCurrentUserInfoOutput
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("depts")]
        public List<string> Depts { get; set; }

        [JsonPropertyName("features")]
        public List<string> Features { get; set; }
    }
}
