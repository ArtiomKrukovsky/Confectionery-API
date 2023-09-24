using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.Services.Interfaces;
using Confectionery.API.Application.ViewModels.Authentication;
using Confectionery.Domain.IRepositories;
using FluentValidation;
using MediatR;

namespace Confectionery.API.Application.Commands.Authentication
{
    public class LogInCommand : ICommand<LogInViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LogInCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

    public class LogInCommandValidation : AbstractValidator<LogInCommand>
    {
        public LogInCommandValidation()
        {
            RuleFor(c => c.Email)
                .Length(3, 550)
                .NotEmpty().WithMessage("Email address is required.");
            RuleFor(s => s.Password)
                .Length(6, 20)
                .NotEmpty().WithMessage("Password is required.");
        }
    }

    public class LogInCommandHandler : IRequestHandler<LogInCommand, LogInViewModel>
    {
        public readonly IClientRepository _clientRepository;
        public readonly IJwtTokenService _jwtTokenService;

        public LogInCommandHandler(IClientRepository clientRepository, IJwtTokenService jwtTokenService)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        }

        public async Task<LogInViewModel> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClientByEmailAsync(request.Email); // todo: get client by email and password

            if (client is null)
            {
                throw new ArgumentException($"Authentication failed, client with set credentials not found.");
            }

            var accessToken = _jwtTokenService.GenerateAccessToken(client);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            return new LogInViewModel(accessToken, refreshToken);
        }
    }
}
