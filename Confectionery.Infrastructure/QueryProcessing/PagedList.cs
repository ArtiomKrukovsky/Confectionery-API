using Confectionery.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;

namespace Confectionery.Infrastructure.QueryProcessing
{
    public class PagedList<TEntity> : IPagedList<TEntity> where TEntity : Entity
    {
        public List<TEntity> Items { get; }
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        public PagedList(
            List<TEntity> items,
            int totalCount,
            int pageNumber,
            int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        public static async Task<IPagedList<TEntity>> ToPagedList(IQueryable<TEntity> source, int pageNumber, int pageSize)
        {
            var totalCount = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<TEntity>(items, totalCount, pageNumber, pageSize);
        }
    }
}
