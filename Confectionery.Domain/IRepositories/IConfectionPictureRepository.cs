using Confectionery.Domain.Entities;
using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.IRepositories
{
    public interface IConfectionPictureRepository : IRepository<ConfectionPicture>
    {
        Task<ConfectionPicture> GetByConfectionIdAsync(Guid confectionId);
    }
}
