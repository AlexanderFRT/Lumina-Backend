using Microsoft.EntityFrameworkCore;

namespace Lumina_BackEnd.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
    }
}
