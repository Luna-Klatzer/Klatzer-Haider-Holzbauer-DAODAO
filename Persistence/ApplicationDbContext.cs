using System.Diagnostics;
using Base;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApplicationDbContext : DbContext
{
    // Using null! to avoid nullable reference warnings
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<HappinessIndex> HappinessIndices { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public ApplicationDbContext() : base()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // We need this for migration
            var connectionString = ConfigurationHelper.GetConfiguration().Get("DefaultConnection", "ConnectionString");

            // For debugging we can enable sensitive data logging
            optionsBuilder.EnableSensitiveDataLogging();

            // Using SQLite as the standard MS SQL LocalDB is not available on Linux
            optionsBuilder.UseMySQL(connectionString);
        }

        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
