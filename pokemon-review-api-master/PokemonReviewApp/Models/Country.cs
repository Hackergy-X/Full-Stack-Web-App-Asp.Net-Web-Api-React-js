using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonReviewApp.Models
{
    public class Country
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Owner> Owners { get; set; }
    }
}
