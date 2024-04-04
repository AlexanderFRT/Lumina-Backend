using Lumina_BackEnd.Models;

namespace Lumina_BackEnd.Repository.User
{
    public interface IUserRepository
    {
        Task<Models.User> GetUser(int id);
        Task<Models.User> AddUser(Models.User usr);
        Task<List<Models.User>> GetUsers();
    }
}
