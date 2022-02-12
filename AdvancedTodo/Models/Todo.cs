using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdvancedTodo.Models
{
    public class Todo
    {
        
        [Range(1, int.MaxValue, ErrorMessage="Please eneter a value bigger then {1}")]
        public int UserId { get; set; }
        
        public int Id { get; set; }
        
        [Required, MaxLength(128)]
        public string Title { get; set; }
        
        
        public bool IsCompleted { get; set; }
        
        
    }
}