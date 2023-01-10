using Confectionery.Domain.Entities;
using Confectionery.Domain.IRepositories;

namespace Confectionery.Infrastructure.Repositories
{
    public class ConfectionRepository : BaseRepository<Confection>, IConfectionRepository
    {
        public ConfectionRepository(СonfectioneryContext context) : base(context)
        {
        }
    }
}
