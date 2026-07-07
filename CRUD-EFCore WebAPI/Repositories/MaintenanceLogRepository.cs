using Microsoft.EntityFrameworkCore;
using CRUDEFCore.Data;
using CRUDEFCore.Models;

namespace CRUDEFCore.Repositories
{
    public class MaintenanceLogRepository : Repository<MaintenanceLog>, IMaintenanceLogRepository
    {
        public MaintenanceLogRepository(AppDbContext db) : base(db) { }

        public async Task<List<MaintenanceLog>> GetByEquipmentIdAsync(int equipmentId) =>
            await _db.MaintenanceLogs
                .Include(m => m.Equipment)
                .Where(m => m.EquipmentId == equipmentId)
                .OrderByDescending(m => m.MaintenanceDate)
                .ToListAsync();

        public async Task<List<MaintenanceLog>> GetAllWithEquipmentAsync() =>
            await _db.MaintenanceLogs
                .Include(m => m.Equipment)
                .OrderByDescending(m => m.MaintenanceDate)
                .ToListAsync();
    }
}
