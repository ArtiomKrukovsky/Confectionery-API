using Confectionery.API.Application.Interfaces;
using Confectionery.Domain.IRepositories;
using FluentValidation;
using MediatR;
using System.Text.RegularExpressions;

namespace Confectionery.API.Application.Queries.User
{
    public class CreateUserCommand : ICommand<Guid>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string InstagramProfile { get; set; }
        public string MobileNumber { get; set; }

        public CreateUserCommand(
            string fullName,
            string email,
            string instagramProfile,
            string mobileNumber)
        {
            FullName = fullName;
            Email = email;
            InstagramProfile = instagramProfile;
            MobileNumber = mobileNumber;
        }

        public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
        {
            public CreateUserCommandValidation()
            {   
                RuleFor(c => c.FullName)
                    .Length(3, 550)
                    .NotEmpty().WithMessage("Full name is required.");
                RuleFor(s => s.Email)
                    .NotEmpty().WithMessage("Email address is required.")
                    .EmailAddress().WithMessage("Email is not required.");
                RuleFor(c => c.MobileNumber)
                    .NotEmpty()
                    .NotNull().WithMessage("Mobile number is required.")
                    .MinimumLength(7).WithMessage("PhoneNumber must not be less than 7 characters.")
                    .MaximumLength(11).WithMessage("PhoneNumber must not exceed 11 characters.")
                    .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("Mobile number is not valid");
            }
        }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
        {
            private readonly IUserRepository _userRepository;

            public CreateUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            }

            public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserByEmailAsync(request.Email);

                if (user is not null)
                {
                    throw new ArgumentException($"Unable to create a user account. " +
                        $"The user with the following email:'{request.Email}' is exist");
                }

                user = new Domain.Entities.User(
                    request.FullName,
                    request.Email,
                    request.InstagramProfile,
                    request.MobileNumber);

                await _userRepository.CreateAsync(user);
                await _userRepository.SaveChangesAsync(cancellationToken);

                return user.Id;
            }
        }
    }
}
