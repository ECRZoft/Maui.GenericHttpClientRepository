using System.ComponentModel.DataAnnotations;

namespace ItemWebApi.Models
{
    public class Todo
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Notes { get; set; }

        public bool IsComplete { get; set; }
    }
}
