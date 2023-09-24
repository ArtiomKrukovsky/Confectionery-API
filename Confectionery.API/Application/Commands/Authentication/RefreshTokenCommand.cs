using Confectionery.API.Application.Constants;
using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.Services.Interfaces;
using Confectionery.API.Application.ViewModels.Authentication;
using Confectionery.Domain.IRepositories;
using FluentValidation;
using MediatR;

namespace Confectionery.API.Application.Commands.Authentication
{
    public class RefreshTokenCommand : ICommand<RefreshTokenViewModel>
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public RefreshTokenCommand(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }

    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(c => c.AccessToken)
                .NotEmpty().NotNull().WithMessage("Invalid client request.");
            RuleFor(s => s.RefreshToken)
                .NotEmpty().NotNull().WithMessage("Invalid client request.");
        }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenViewModel>
    {
        public readonly IClientRepository _clientRepository;
        public readonly IJwtTokenService _jwtTokenService;

        public RefreshTokenCommandHandler(IClientRepository clientRepository, IJwtTokenService jwtTokenService)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        }

        public async Task<RefreshTokenViewModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _jwtTokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            var email = principal.FindFirst(JwtClaimNames.UserNameClaimName);

            var client = await _clientRepository.GetClientByEmailAsync(email!.Value);

            //if (client is null || client.RefreshToken != request.RefreshToken || client.RefreshTokenExpirationTime <= DateTime.Now)
            //{
            //    throw new BadHttpRequestException("Invalid client request.");
            //}

            var newAccessToken = _jwtTokenService.GenerateAccessToken(client);
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

            return new RefreshTokenViewModel(newAccessToken, newRefreshToken);
        }
    }
}
