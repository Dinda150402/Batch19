using CRUDEFCore.Models;

namespace CRUDEFCore.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<List<Department>> GetAllWithEmployeesAsync();
        Task<Department?> GetByIdWithEmployeesAsync(int id);
        Task<bool> ExistsByNameAsync(string name);
    }
}
