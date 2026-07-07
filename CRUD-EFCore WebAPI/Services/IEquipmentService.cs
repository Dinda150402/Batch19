using CRUDEFCore.Common;
using CRUDEFCore.DTOs;

namespace CRUDEFCore.Services
{
    public interface IEquipmentService
    {
        Task<ServiceResult<List<EquipmentReadDto>>> GetAllEquipmentsAsync();
        Task<ServiceResult<EquipmentReadDto>> GetEquipmentByIdAsync(int id);
        Task<ServiceResult<List<EquipmentReadDto>>> GetEquipmentsByDepartmentAsync(int departmentId);
        Task<ServiceResult<List<EquipmentReadDto>>> SearchEquipmentByNameAsync(string keyword);
        Task<ServiceResult<EquipmentReadDto>> CreateEquipmentAsync(EquipmentCreateDto dto);
        Task<ServiceResult> UpdateEquipmentAsync(int id, EquipmentUpdateDto dto);
        Task<ServiceResult> DeleteEquipmentAsync(int id);
        Task<ServiceResult> AssignEquipmentToEmployeeAsync(int equipmentId, int employeeId);
    }
}
