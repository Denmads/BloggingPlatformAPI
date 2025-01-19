using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class Post : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
