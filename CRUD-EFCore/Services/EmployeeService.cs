using CRUDEFCore.Data;
using CRUDEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDEFCore.Services
{
    public class EmployeeService
    {
        private readonly AppDbContext _db;

        public EmployeeService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Employee> AddAsync(string name, string department)
        {
            var employee = new Employee { Name = name, Department = department };
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            return employee;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _db.Employees
                .Include(e => e.EquipmentList)
                .OrderBy(e => e.Id)
                .ToListAsync();
        }

        public async Task<List<Employee>> FilterByDepartmentAsync(string keyword)
        {
            return await _db.Employees
                .Where(e => e.Department.Contains(keyword))
                .ToListAsync();
        }
    }
}