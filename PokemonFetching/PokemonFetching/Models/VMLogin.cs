using System.ComponentModel.DataAnnotations;

namespace PokemonFetching.Models
{
    public class VMLogin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool KeepLoggedIn { get; set; }
    }
}
