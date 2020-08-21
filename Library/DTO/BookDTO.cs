using System.Text.Json.Serialization;

namespace Library.DTO
{
    public class BookDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("author")]
        public string Author { get; set; }
        
        [JsonPropertyName("authorId")]
        public int AuthorId { get; set; }
    }
}