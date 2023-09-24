using Confectionery.API.Application.Interfaces;
using Confectionery.Domain.IRepositories;
using FluentValidation;
using MediatR;
using System.Text.RegularExpressions;

namespace Confectionery.API.Application.Queries.Client
{
    public class CreateClientCommand : ICommand<Guid>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string InstagramProfile { get; set; }
        public string MobileNumber { get; set; }

        public CreateClientCommand(
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

        public class CreateClientCommandValidation : AbstractValidator<CreateClientCommand>
        {
            public CreateClientCommandValidation()
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

        public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Guid>
        {
            private readonly IClientRepository _clientRepository;

            public CreateClientCommandHandler(IClientRepository clientRepository)
            {
                _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            }

            public async Task<Guid> Handle(CreateClientCommand request, CancellationToken cancellationToken)
            {
                var client = await _clientRepository.GetClientByEmailAsync(request.Email);

                if (client is not null)
                {
                    throw new ArgumentException($"Unable to create a client account. " +
                        $"The client with the following email:'{request.Email}' is exist");
                }

                client = new Domain.Entities.Client(
                    request.FullName,
                    request.Email,
                    request.InstagramProfile,
                    request.MobileNumber);

                await _clientRepository.CreateAsync(client);
                await _clientRepository.SaveChangesAsync(cancellationToken);

                return client.Id;
            }
        }
    }
}
