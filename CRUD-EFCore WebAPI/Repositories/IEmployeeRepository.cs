using CRUDEFCore.Models;

namespace CRUDEFCore.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee?> GetByIdWithDetailsAsync(int id);
        Task<List<Employee>> GetAllWithDetailsAsync();
        Task<List<Employee>> GetByDepartmentIdAsync(int departmentId);
    }
}
