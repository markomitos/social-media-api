using System.ComponentModel.DataAnnotations;

namespace SocialMediaAPI.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
