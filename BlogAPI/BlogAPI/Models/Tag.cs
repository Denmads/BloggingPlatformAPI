using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
