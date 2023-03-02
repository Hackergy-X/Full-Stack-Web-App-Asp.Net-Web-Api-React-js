using System.ComponentModel.DataAnnotations;

namespace PokemonFetching.Models
{
    public class Registration
    {
        public int Id { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public int IsActive { get; set; }

        public bool KeepLoggedIn { get; set; }
    }
}
