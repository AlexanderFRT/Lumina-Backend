using Lumina_BackEnd.Data;
using Microsoft.EntityFrameworkCore;

namespace Lumina_BackEnd.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _context;

        public UserRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Models.User> GetUser(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<Models.User> AddUser(Models.User usr)
        {
            _context.User.Add(usr);
            await _context.SaveChangesAsync();
            return usr;
        }

        public async Task<List<Models.User>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }
    }
}
