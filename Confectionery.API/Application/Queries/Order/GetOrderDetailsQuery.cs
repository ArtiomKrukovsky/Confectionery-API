using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.ViewModels;
using Confectionery.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using System.Runtime.Serialization;

namespace Confectionery.API.Application.Queries.Order
{
    [DataContract]
    public class GetOrderDetailsQuery : IQuery<List<OrderDetailViewModel>>
    {
        public GetOrderDetailsQuery()
        {
        }
    }

    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, List<OrderDetailViewModel>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderDetailsQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<List<OrderDetailViewModel>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var ordersWithDetails = await _orderRepository.GetOrdersWithDetailsAsync();

            return _mapper.Map<List<OrderDetailViewModel>>(ordersWithDetails);
        }
    }
}
