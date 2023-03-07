using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_ba.Models
{
    public class Staff
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }
    }
}
