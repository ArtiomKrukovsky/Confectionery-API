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
                .NotEmpty().NotNull().WithMessage("Invalid user request.");
            RuleFor(s => s.RefreshToken)
                .NotEmpty().NotNull().WithMessage("Invalid user request.");
        }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenViewModel>
    {
        public readonly IUserRepository _userRepository;
        public readonly IJwtTokenService _jwtTokenService;

        public RefreshTokenCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        }

        public async Task<RefreshTokenViewModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _jwtTokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            var email = principal.FindFirst(JwtClaimNames.UserNameClaimName);

            var user = await _userRepository.GetUserWithTokenByEmailAsync(email!.Value);

            if (user is null || user.RefreshToken.Token != request.RefreshToken || user.RefreshToken.ExpirationTime <= DateTime.Now)
            {
                throw new BadHttpRequestException("Invalid user request.");
            }

            var newAccessToken = _jwtTokenService.GenerateAccessToken(user);
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

            return new RefreshTokenViewModel(newAccessToken, newRefreshToken);
        }
    }
}
