using CRUDEFCore.Common;
using CRUDEFCore.DTOs;

namespace CRUDEFCore.Services
{
    public interface IEmployeeService
    {
        Task<ServiceResult<List<EmployeeReadDto>>> GetAllEmployeesAsync();
        Task<ServiceResult<EmployeeReadDto>> GetEmployeeByIdAsync(int id);
        Task<ServiceResult<List<EmployeeReadDto>>> GetEmployeesByDepartmentAsync(int departmentId);
        Task<ServiceResult<EmployeeReadDto>> CreateEmployeeAsync(EmployeeCreateDto dto);
    }
}
