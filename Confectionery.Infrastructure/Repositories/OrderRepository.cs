using Confectionery.Domain.Entities;
using Confectionery.Domain.IRepositories;

namespace Confectionery.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(СonfectioneryContext context) : base(context)
        {
        }
    }
}
