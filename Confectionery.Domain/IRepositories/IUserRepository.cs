using Confectionery.Domain.Entities;
using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserWithTokenByEmailAsync(string email);
        Task<User> GetUserByCredentialsAsync(string email, string passwordHash);
    }
}
