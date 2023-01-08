using MediatR;

namespace Confectionery.API.Application.Interfaces
{
    public interface IQuery<out T> : IRequest<T>
    {
    }
}
