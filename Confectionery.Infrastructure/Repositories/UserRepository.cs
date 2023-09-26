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

        public async Task<User> GetUserWithTokenByEmailAsync(string email)
        {
            return await _context.Users
                .Include(x => x.RefreshToken)
                .FirstOrDefaultAsync(user =>
                    user.Email == email
                );
        }

        public async Task<User> GetUserByCredentialsAsync(string email, string passwordHash)
        {
            return await _context.Users
                .FirstOrDefaultAsync(
                    user => user.Email == email && 
                    user.PasswordHash == passwordHash
                );
        }
    }
}
