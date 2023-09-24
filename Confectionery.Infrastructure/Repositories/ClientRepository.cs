using Confectionery.Domain.Entities;
using Confectionery.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Confectionery.Infrastructure.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(СonfectioneryContext context) : base(context)
        {
        }

        public async Task<Client> GetClientByEmailAsync(string email)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(client => 
                    client.Email == email
                );
        }
    }
}
