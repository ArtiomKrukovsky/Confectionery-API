using Confectionery.Domain.Entities;
using Confectionery.Domain.IRepositories;
using Confectionery.Domain.Seedwork;
using Confectionery.Infrastructure.QueryProcessing;
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
                .Include(x => x.Client)
                .Include(x => x.Confection)
                .ToListAsync();
        }

        public async Task<IPagedList<Order>> GetOrdersWithDetailsAsync(IQueryParameters queryParameters)
        {
            var ordersWithDetailsQuery = _context.Orders
                .Include(x => x.Client)
                .Include(x => x.Confection)
                .AsQueryable();

            if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            {
                ordersWithDetailsQuery = ordersWithDetailsQuery.Where(order =>
                    order.Client.FullName.Contains(queryParameters.SearchTerm) ||
                    order.Confection.Name.Contains(queryParameters.SearchTerm));
            }

            return await PagedList<Order>.ToPagedList(
                ordersWithDetailsQuery,
                queryParameters.PageNumber,
                queryParameters.PageSize);
        }
    }
}
