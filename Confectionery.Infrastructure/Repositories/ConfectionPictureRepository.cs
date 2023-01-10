using Confectionery.Domain.Entities;
using Confectionery.Domain.IRepositories;

namespace Confectionery.Infrastructure.Repositories
{
    public class ConfectionPictureRepository : BaseRepository<ConfectionPicture>, IConfectionPictureRepository
    {
        public ConfectionPictureRepository(СonfectioneryContext context) : base(context)
        {
        }
    }
}
