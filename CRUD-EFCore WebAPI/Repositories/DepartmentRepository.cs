using Microsoft.EntityFrameworkCore;
using CRUDEFCore.Data;
using CRUDEFCore.Models;

namespace CRUDEFCore.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext db) : base(db) { }

        public async Task<List<Department>> GetAllWithEmployeesAsync() =>
            await _db.Departments
                .Include(d => d.Employees)
                .OrderBy(d => d.Id)
                .ToListAsync();

        public async Task<Department?> GetByIdWithEmployeesAsync(int id) =>
            await _db.Departments
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id);

        public async Task<bool> ExistsByNameAsync(string name) =>
            await _db.Departments
                .AnyAsync(d => d.Name.ToLower() == name.ToLower());
    }
}
