using Confectionery.API.Application.Interfaces;
using Confectionery.Domain.IRepositories;
using FluentValidation;
using MediatR;

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
