using CRUDEFCore.Data;
using CRUDEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDEFCore.Services
{
    public class EquipmentService
    {
        private readonly AppDbContext _db;

        public EquipmentService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Equipment> AddAsync(string name, string serial, string? requiredDepartment)
        {
            var equipment = new Equipment
            {
                Name = name,
                SerialNumber = serial,
                LastCalibrationDate = DateTime.Now,
                RequiredDepartment = requiredDepartment
            };
            _db.Equipments.Add(equipment);
            await _db.SaveChangesAsync();
            return equipment;
        }

        public async Task<List<Equipment>> GetAllAsync()
        {
            return await _db.Equipments
                .Include(e => e.Employees)
                .OrderBy(e => e.Id)
                .ToListAsync();
        }

        public async Task<List<Equipment>> SearchByNameAsync(string keyword)
        {
            return await _db.Equipments
                .Include(e => e.Employees)
                .Where(e => e.Name.Contains(keyword))
                .ToListAsync();
        }

        public async Task<bool> UpdateNameAsync(int id, string newName)
        {
            var equipment = await _db.Equipments.FindAsync(id);
            if (equipment == null) return false;

            equipment.Name = newName;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var equipment = await _db.Equipments.FindAsync(id);
            if (equipment == null) return false;

            _db.Equipments.Remove(equipment);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<(bool success, string message)> AssignToEmployeeAsync(int equipmentId, int employeeId)
        {
            var equipment = await _db.Equipments
                .Include(e => e.Employees)
                .FirstOrDefaultAsync(e => e.Id == equipmentId);
            var employee = await _db.Employees.FindAsync(employeeId);

            if (equipment == null || employee == null)
                return (false, "ID tidak ditemukan.");

            if (equipment.RequiredDepartment != null &&
                !equipment.RequiredDepartment.Equals(employee.Department, StringComparison.OrdinalIgnoreCase))
            {
                return (false, $"{equipment.Name} hanya boleh dipakai department '{equipment.RequiredDepartment}', sedangkan {employee.Name} dari department '{employee.Department}'.");
            }

            if (equipment.Employees.Any(e => e.Id == employeeId))
                return (false, $"{employee.Name} sudah di-assign ke {equipment.Name} sebelumnya.");

            equipment.Employees.Add(employee);
            await _db.SaveChangesAsync();
            return (true, $"{employee.Name} berhasil di-assign ke {equipment.Name}");
        }
    }
}