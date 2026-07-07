using CRUDEFCore.Models;

namespace CRUDEFCore.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
    }
}
