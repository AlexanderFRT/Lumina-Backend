using Lumina_Backend.Data;
using Lumina_Backend.ModelsDTO;
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
               var addedUser = await _context.Users.AddAsync(user);
               await _context.SaveChangesAsync();
               
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

        public async Task<Login> login(Login login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == login.UserName && u.Password == login.Password);
            if (user != null)
            {
                return new Login()
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    SessionToken = user.SessionToken
                };
            }
            else
            {
                return null;
            }
        }
    }
}
