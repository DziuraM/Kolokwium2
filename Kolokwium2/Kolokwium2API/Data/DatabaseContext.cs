using Kolokwium2API.Models;

namespace Kolokwium2API.Data;

using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    public DbSet<Nursery> Nurseries { get; set; }
    public DbSet<SeedlingBatch> SeedlingBatches { get; set; }
    public DbSet<TreeSpecies> TreeSpecies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Responsible> Responsibles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Responsible>()
            .HasKey(r => new { r.BatchId, r.EmployeeId });

        modelBuilder.Entity<Responsible>()
            .HasOne(r => r.Batch)
            .WithMany(b => b.Responsibles)
            .HasForeignKey(r => r.BatchId);

        modelBuilder.Entity<Responsible>()
            .HasOne(r => r.Employee)
            .WithMany(e => e.Responsibles)
            .HasForeignKey(r => r.EmployeeId);

        modelBuilder.Entity<Nursery>().HasData(
            new Nursery { NurseryId = 1, Name = "GreenGrow", EstablishedDate = new DateTime(2010, 1, 1) }
        );

        modelBuilder.Entity<TreeSpecies>().HasData(
            new TreeSpecies { SpeciesId = 1, LatinName = "Pinus sylvestris", GrowthTimeInYears = 5 },
            new TreeSpecies { SpeciesId = 2, LatinName = "Quercus robur", GrowthTimeInYears = 8 }
        );

        modelBuilder.Entity<Employee>().HasData(
            new Employee { EmployeeId = 1, FirstName = "Anna", LastName = "Nowak", HireDate = new DateTime(2020, 5, 15) },
            new Employee { EmployeeId = 2, FirstName = "Jan", LastName = "Kowalski", HireDate = new DateTime(2018, 9, 10) }
        );

        modelBuilder.Entity<SeedlingBatch>().HasData(
            new SeedlingBatch
            {
                BatchId = 1,
                NurseryId = 1,
                SpeciesId = 1,
                Quantity = 1000,
                SownDate = new DateTime(2024, 3, 1),
                ReadyDate = new DateTime(2029, 3, 1)
            }
        );

        modelBuilder.Entity<Responsible>().HasData(
            new Responsible { BatchId = 1, EmployeeId = 1, Role = "Supervisor" },
            new Responsible { BatchId = 1, EmployeeId = 2, Role = "Technician" }
        );
    }
}