using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_ba.Models
{
    public class Registration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public int IsActive { get; set; }
        public int ISApproved { get; set; }

    }
}
