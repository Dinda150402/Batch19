using Microsoft.EntityFrameworkCore;
using CRUDEFCore.Models;

namespace CRUDEFCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.Employees)
                .WithMany(e => e.EquipmentList)
                .UsingEntity(j => j.ToTable("EquipmentEmployee"));

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.RequiredDepartment)
                .WithMany(d => d.RestrictedEquipments)
                .HasForeignKey(e => e.RequiredDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MaintenanceLog>()
                .HasOne(m => m.Equipment)
                .WithMany(e => e.MaintenanceLogs)
                .HasForeignKey(m => m.EquipmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
