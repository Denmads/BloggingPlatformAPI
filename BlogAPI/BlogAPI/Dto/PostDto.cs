using System.ComponentModel.DataAnnotations;

namespace BlogAPI.DTO
{
    public class PostDto
    {

        [Required]
        [StringLength(100, ErrorMessage = "Title can't be more than 100 characters")]
        public string Title { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Content can't be more than 2000 characters")]
        public string Content { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Category can't be more than 50 characters")]
        public string Category { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "Can't have more than 5 tags")]
        public List<string> Tags { get; set; }
    }
}
