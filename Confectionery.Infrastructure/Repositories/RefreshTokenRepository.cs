using Confectionery.Domain.Entities;
using Confectionery.Domain.IRepositories;

namespace Confectionery.Infrastructure.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(СonfectioneryContext context) : base(context)
        {
        }
    }
}
