namespace Confectionery.Domain.Seedwork
{
    public interface IQueryParameters
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string? SearchTerm { get; }
    }
}
