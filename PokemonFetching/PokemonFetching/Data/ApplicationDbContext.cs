
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokemonFetching.Models;

namespace PokemonFetching.Data
{

    public class ApplicationDbContext: IdentityDbContext
    {
        
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Registration> Registration { get; set; }
    }
}
