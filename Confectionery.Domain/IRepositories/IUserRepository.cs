using Confectionery.Domain.Entities;
using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
    }
}
