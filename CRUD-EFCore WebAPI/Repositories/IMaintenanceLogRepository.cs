using CRUDEFCore.Models;

namespace CRUDEFCore.Repositories
{
    public interface IMaintenanceLogRepository : IRepository<MaintenanceLog>
    {
        Task<List<MaintenanceLog>> GetByEquipmentIdAsync(int equipmentId);
        Task<List<MaintenanceLog>> GetAllWithEquipmentAsync();
    }
}
