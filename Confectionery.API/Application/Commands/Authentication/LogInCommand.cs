using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.Services.Interfaces;
using Confectionery.API.Application.ViewModels.Authentication;
using Confectionery.Domain.IRepositories;
using FluentValidation;
using MediatR;
using System.Security.Cryptography;
using System.Text;

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
        public readonly IUserRepository _userRepository;
        public readonly IJwtTokenService _jwtTokenService;

        public LogInCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        }

        public async Task<LogInViewModel> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = GeneratePasswordHash(request.Password);
            var user = await _userRepository.GetUserByCredentialsAsync(request.Email, passwordHash);

            if (user is null)
            {
                throw new ArgumentException($"Authentication failed, user with set credentials not found.");
            }

            var accessToken = _jwtTokenService.GenerateAccessToken(user);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            return new LogInViewModel(accessToken, refreshToken);
        }

        private string GeneratePasswordHash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return Convert.ToBase64String(hashBytes);
        }
    }
}
