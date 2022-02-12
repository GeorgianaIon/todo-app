using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Todo
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage="Please enter a value bigger then {1}")]
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }
       
        [JsonPropertyName("id"), Key]
        [Required]
        public int TodoId { get; set; }
        
        [Required, MaxLength(128)]
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("IsCompleted"), Required]
        public bool IsCompleted { get; set; }
        
        
    }
}