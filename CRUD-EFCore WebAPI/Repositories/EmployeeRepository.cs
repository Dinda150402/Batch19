using Microsoft.EntityFrameworkCore;
using CRUDEFCore.Data;
using CRUDEFCore.Models;

namespace CRUDEFCore.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext db) : base(db) { }

        public async Task<Employee?> GetByIdWithDetailsAsync(int id) =>
            await _db.Employees
                .Include(e => e.Department)
                .Include(e => e.EquipmentList)
                .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<List<Employee>> GetAllWithDetailsAsync() =>
            await _db.Employees
                .Include(e => e.Department)
                .Include(e => e.EquipmentList)
                .OrderBy(e => e.Name)
                .ToListAsync();

        public async Task<List<Employee>> GetByDepartmentIdAsync(int departmentId) =>
            await _db.Employees
                .Include(e => e.Department)
                .Include(e => e.EquipmentList)
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();
    }
}
