using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CRUDEFCore.Models;

namespace CRUDEFCore.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // WAJIB dipanggil duluan - ini yang bikin tabel-tabel Identity
            // (AspNetUsers, AspNetRoles, AspNetUserRoles, dst) ke-generate
            base.OnModelCreating(modelBuilder);

            // Many-to-many: Equipment <-> Employee
            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.Employees)
                .WithMany(e => e.EquipmentList)
                .UsingEntity(j => j.ToTable("EquipmentEmployee"));

            // One-to-many: Department -> Employee (wajib)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many opsional: Department -> Equipment (RequiredDepartment)
            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.RequiredDepartment)
                .WithMany(d => d.RestrictedEquipments)
                .HasForeignKey(e => e.RequiredDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many: Equipment -> MaintenanceLog (cascade delete)
            modelBuilder.Entity<MaintenanceLog>()
                .HasOne(m => m.Equipment)
                .WithMany(e => e.MaintenanceLogs)
                .HasForeignKey(m => m.EquipmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
