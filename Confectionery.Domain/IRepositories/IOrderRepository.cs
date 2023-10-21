using Confectionery.Domain.Entities;
using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.IRepositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersWithDetailsAsync();
        Task<IPagedList<Order>> GetOrdersWithDetailsAsync(IQueryParameters queryParameters);
    }
}
