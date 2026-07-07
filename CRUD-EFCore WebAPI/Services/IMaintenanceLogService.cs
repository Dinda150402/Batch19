using CRUDEFCore.Common;
using CRUDEFCore.DTOs;

namespace CRUDEFCore.Services
{
    public interface IMaintenanceLogService
    {
        Task<ServiceResult<List<MaintenanceLogReadDto>>> GetAllLogsAsync();
        Task<ServiceResult<List<MaintenanceLogReadDto>>> GetLogsByEquipmentIdAsync(int equipmentId);
        Task<ServiceResult<MaintenanceLogReadDto>> CreateLogAsync(MaintenanceLogCreateDto dto);
    }
}
