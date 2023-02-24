


using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure.Data;

public class DataContext : IdentityDbContext<IdentityUser>
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Language> Languages { get; set; } 
    public DbSet<Translate> Translations { get; set; }
    public DbSet<Group> Groups { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Translate>().HasMany(e => e.Languages).WithOne(d => d.Translate);

        base.OnModelCreating(modelBuilder);
    }
}