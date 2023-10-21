using Confectionery.Domain.Entities;
using Confectionery.Domain.IRepositories;
using Confectionery.Domain.Seedwork;
using Confectionery.Infrastructure.QueryProcessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Confectionery.Infrastructure.Repositories
{
    public class ConfectionRepository : BaseRepository<Confection>, IConfectionRepository
    {
        public ConfectionRepository(СonfectioneryContext context) : base(context)
        {
        }

        public async Task<List<Confection>> GetConfectionsWithPicturesAsync()
        {
            return await _context.Confections
                .Include(x => x.Pictures)
                .ToListAsync();
        }

        public async Task<IPagedList<Confection>> GetConfectionsWithPicturesAsync(IQueryParameters queryParameters)
        {
            var confectionsWithPicturesQuery = _context.Confections
                .Include(x => x.Pictures)
                .AsQueryable();

            if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            {
                confectionsWithPicturesQuery = confectionsWithPicturesQuery.Where(confection =>
                    confection.Name.Contains(queryParameters.SearchTerm) ||
                    confection.Description.Contains(queryParameters.SearchTerm));
            }

            return await PagedList<Confection>.ToPagedList(
                confectionsWithPicturesQuery,
                queryParameters.PageNumber,
                queryParameters.PageSize);
        }

        public async Task<Confection> GetConfectionWithPicturesAsync(Guid confectionId)
        {
            return await _context.Confections
                .Include(x => x.Pictures)
                .FirstOrDefaultAsync(x => x.Id == confectionId);
        }
    }
}
