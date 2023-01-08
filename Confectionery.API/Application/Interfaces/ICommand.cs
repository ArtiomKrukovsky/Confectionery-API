using MediatR;

namespace Confectionery.API.Application.Interfaces
{
    public interface ICommand<out T> : IRequest<T>
    {
    }

    public interface ICommand : IRequest
    {
    }
}
