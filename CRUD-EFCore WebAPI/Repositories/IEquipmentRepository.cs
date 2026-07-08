using CRUDEFCore.Models;

namespace CRUDEFCore.Repositories
{
    public interface IEquipmentRepository : IRepository<Equipment>
    {
        Task<Equipment?> GetByIdWithDetailsAsync(int id);
        Task<List<Equipment>> GetAllWithDetailsAsync();
        Task<List<Equipment>> SearchByNameAsync(string keyword);
        Task<List<Equipment>> GetByRequiredDepartmentIdAsync(int departmentId);
    }
}
