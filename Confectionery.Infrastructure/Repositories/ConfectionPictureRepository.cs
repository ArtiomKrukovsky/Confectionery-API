using Confectionery.Domain.Entities;
using Confectionery.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Confectionery.Infrastructure.Repositories
{
    public class ConfectionPictureRepository : BaseRepository<ConfectionPicture>, IConfectionPictureRepository
    {
        public ConfectionPictureRepository(СonfectioneryContext context) : base(context)
        {
        }

        public async Task<ConfectionPicture> GetByConfectionIdAsync(Guid confectionId)
        {
            return await _context.ConfectionPictures
                .FirstOrDefaultAsync(picture =>
                    picture.ConfectionId == confectionId
                );
        }
    }
}
