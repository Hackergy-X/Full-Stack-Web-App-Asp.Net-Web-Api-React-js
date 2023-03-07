using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_ba.Models
{
    public class Article
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public int IsActive { get; set; }
        public int ISApproved { get; set; }
        public string type { get; set; }
    }
}
