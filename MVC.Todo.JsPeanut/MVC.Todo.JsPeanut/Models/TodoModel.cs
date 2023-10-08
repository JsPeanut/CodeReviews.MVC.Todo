using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MVC.Todo.JsPeanut.Models
{
    public class TodoModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        [Display(Name = "To do name")]
        public string Name { get; set; }

        [JsonPropertyName("isComplete")]
        public bool IsComplete { get; set; }
    }
}
