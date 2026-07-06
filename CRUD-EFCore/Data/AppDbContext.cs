using Microsoft.EntityFrameworkCore;
using CRUDEFCore.Models;

namespace CRUDEFCore.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=crud.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.Employees)
                .WithMany(e => e.EquipmentList)
                .UsingEntity(j => j.ToTable("EquipmentEmployee"));
        }
    }
}