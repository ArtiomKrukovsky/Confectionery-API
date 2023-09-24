using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.ViewModels;
using Confectionery.Domain.IRepositories;
using MapsterMapper;
using MediatR;

namespace Confectionery.API.Application.Queries.Client
{
    public class GetClientQuery : IQuery<ClientViewModel>
    {
        public string Email { get; set; }

        public GetClientQuery(string email)
        {
            Email = email;
        }
    }

    public class GetClientQueryHandler : IRequestHandler<GetClientQuery, ClientViewModel>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ClientViewModel> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClientByEmailAsync(request.Email);

            return _mapper.Map<ClientViewModel>(client);
        }
    }
}
