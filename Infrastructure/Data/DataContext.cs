


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : IdentityDbContext<IdentityUser>
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    // public DbSet<Country> Countries  { get; set; }


   
    
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Casts>()
    //         .HasKey(c => new { c.ActorId, c.MovieId });
    //     base.OnModelCreating(modelBuilder);
    // }
}