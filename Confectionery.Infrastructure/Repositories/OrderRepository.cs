using Confectionery.Domain.Entities;
using Confectionery.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Confectionery.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(СonfectioneryContext context) : base(context)
        {
        }

        public async Task<List<Order>> GetOrdersWithDetailsAsync()
        {
            return await _context.Orders
                .Include(x => x.User)
                .Include(x => x.Confection)
                .ToListAsync();
        }
    }
}
