namespace Confectionery.Domain.Seedwork
{
    public interface IPagedList<TEntity> where TEntity : Entity
    {
        public List<TEntity> Items { get; }
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }
    }
}
