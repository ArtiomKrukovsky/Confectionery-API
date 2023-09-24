using Confectionery.Domain.Entities;
using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.IRepositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> GetClientByEmailAsync(string email);
    }
}
