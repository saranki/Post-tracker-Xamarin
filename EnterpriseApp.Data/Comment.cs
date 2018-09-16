using Newtonsoft.Json;

namespace EnterpriseApp.Data
{
    public class Comment
    {
        [JsonProperty(PropertyName = "postId")]
        public int? PostId { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
    }
}
