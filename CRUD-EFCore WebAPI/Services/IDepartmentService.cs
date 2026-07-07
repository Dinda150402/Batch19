using CRUDEFCore.Common;
using CRUDEFCore.DTOs;

namespace CRUDEFCore.Services
{
    public interface IDepartmentService
    {
        Task<ServiceResult<List<DepartmentReadDto>>> GetAllDepartmentsAsync();
        Task<ServiceResult<DepartmentReadDto>> GetDepartmentByIdAsync(int id);
        Task<ServiceResult<DepartmentReadDto>> CreateDepartmentAsync(DepartmentCreateDto dto);
    }
}
