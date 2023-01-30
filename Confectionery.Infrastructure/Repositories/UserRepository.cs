using Confectionery.Domain.Entities;
using Confectionery.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Confectionery.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(СonfectioneryContext context) : base(context)
        {
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(user => 
                    user.Email.Equals(email, StringComparison.OrdinalIgnoreCase)
                );
        }
    }
}
