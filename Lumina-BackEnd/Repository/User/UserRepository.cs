using Lumina_BackEnd.Data;

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
            return await _context.Users.FindAsync(id);
        }

        public async Task<Models.User> AddUser(Models.User usr)
        {
            _context.Users.Add(usr);
            await _context.SaveChangesAsync();
            return usr;
        }

        public async Task<List<Models.User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
