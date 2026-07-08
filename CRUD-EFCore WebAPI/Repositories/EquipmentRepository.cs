using Microsoft.EntityFrameworkCore;
using CRUDEFCore.Data;
using CRUDEFCore.Models;

namespace CRUDEFCore.Repositories
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(AppDbContext db) : base(db) { }

        public async Task<Equipment?> GetByIdWithDetailsAsync(int id) =>
            await _db.Equipments
                .Include(e => e.Employees)
                .Include(e => e.RequiredDepartment)
                .Include(e => e.MaintenanceLogs)
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<List<Equipment>> GetAllWithDetailsAsync() =>
            await _db.Equipments
                .Include(e => e.Employees)
                .Include(e => e.RequiredDepartment)
                .Include(e => e.MaintenanceLogs)
                .OrderBy(e => e.Id)
                .ToListAsync();

        public async Task<List<Equipment>> SearchByNameAsync(string keyword) =>
            await _db.Equipments
                .Include(e => e.Employees)
                .Include(e => e.RequiredDepartment)
                .Include(e => e.MaintenanceLogs)
                .Where(e => e.Name.Contains(keyword))
                .ToListAsync();

        public async Task<List<Equipment>> GetByRequiredDepartmentIdAsync(int departmentId) =>
            await _db.Equipments
                .Include(e => e.Employees)
                .Include(e => e.RequiredDepartment)
                .Include(e => e.MaintenanceLogs)
                .Where(e => e.RequiredDepartmentId == departmentId)
                .ToListAsync();
    }
}
