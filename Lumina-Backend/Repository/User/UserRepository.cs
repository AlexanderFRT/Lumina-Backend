using Lumina_Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Lumina_Backend.Repository.User
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
            return await _context.Users.FindAsync(id);
        }

        public async Task<Models.User> AddUser(Models.User user)
        {
            try
            {
               var addedUser = await this._context.Users.AddAsync(user);
               await this._context.SaveChangesAsync();
               
                return user;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public async Task<List<Models.User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
