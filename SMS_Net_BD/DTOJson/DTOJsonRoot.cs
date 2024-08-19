using System.Text.Json.Serialization;

namespace SMS_Net_BD.DTOJson
{
    public class JsonRoot
    {

        [JsonPropertyName("data")]
        public Data Data { get; set; }
        [JsonPropertyName("error")]
        public int Error { get; set; }

        [JsonPropertyName("msg")]
        public string Message { get; set; }
    }
    public class Data
    {
        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("camp_id")]
        public string CampId { get; set; }

        [JsonPropertyName("validity")]
        public DateTime Validity { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }

        [JsonPropertyName("updated_by")]
        public int UpdatedBy { get; set; }
    }
}