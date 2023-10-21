using Confectionery.Domain.Entities;
using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.IRepositories
{
    public interface IConfectionRepository : IRepository<Confection>
    {
        Task<List<Confection>> GetConfectionsWithPicturesAsync();
        Task<IPagedList<Confection>> GetConfectionsWithPicturesAsync(IQueryParameters queryParameters);
        Task<Confection> GetConfectionWithPicturesAsync(Guid confectionId);
    }
}
