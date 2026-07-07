using Microsoft.EntityFrameworkCore;
using CRUDEFCore.Data;
using CRUDEFCore.Models;

namespace CRUDEFCore.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db) { }

        public async Task<User?> GetByUsernameAsync(string username) =>
            await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}
