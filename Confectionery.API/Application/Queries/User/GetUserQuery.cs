using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.ViewModels;
using Confectionery.Domain.IRepositories;
using MapsterMapper;
using MediatR;

namespace Confectionery.API.Application.Queries.User
{
    public class GetUserQuery : IQuery<UserViewModel>
    {
        public string Email { get; set; }

        public GetUserQuery(string email)
        {
            Email = email;
        }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
