using Confectionery.Domain.Entities;
using Confectionery.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Confection> GetConfectionWithPicturesAsync(Guid confectionId)
        {
            return await _context.Confections
                .Include(x => x.Pictures)
                .FirstOrDefaultAsync(x => x.Id == confectionId);
        }
    }
}
