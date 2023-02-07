using Confectionery.API.Application.Constants;
using Confectionery.API.Application.Interfaces;
using Confectionery.API.Options;
using Confectionery.Domain.IRepositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Confectionery.API.Application.Commands.Order
{
    public class CreateOrderCommand : ICommand<bool>
    {
        public Guid ConfectionId { get; set; }
        public Guid UserId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public CreateOrderCommand(
            Guid confectionId,
            Guid userId,
            decimal unitPrice,
            int quantity)
        {
            ConfectionId = confectionId;
            UserId = userId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }

    public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidation()
        {
            RuleFor(c => c.ConfectionId).NotNull().NotEmpty();
            RuleFor(c => c.UserId).NotNull().NotEmpty();
            RuleFor(c => c.UnitPrice).NotNull().GreaterThan(0);
            RuleFor(c => c.Quantity).NotNull().GreaterThanOrEqualTo(1);
        }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly EmailSenderOptions _emailSenderOptions;

        public CreateOrderCommandHandler(
            IUserRepository userRepository, 
            IOrderRepository orderRepository,
            IOptions<EmailSenderOptions> emailSenderOptions)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _emailSenderOptions = emailSenderOptions.Value ?? throw new ArgumentNullException(nameof(emailSenderOptions));
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Domain.Entities.Order(
                request.ConfectionId,
                request.UserId,
                request.UnitPrice,
                request.Quantity);

            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveChangesAsync(cancellationToken);

            var user = await _userRepository.GetAsync(request.UserId);
            await SendEmailAsync(user.FullName, user.Email);

            return true;
        }

        private async Task SendEmailAsync(string fullName, string userEmail)
        {
            var fromAddress = new MailAddress(_emailSenderOptions.Address, _emailSenderOptions.Name);
            var toAddress = new MailAddress(userEmail);

            string fromPassword = _emailSenderOptions.Password;
            string subject = EmailConstants.OrderSubject;

            // todo: move to the constants
            string body = $"Здравствуйте, {fullName}. Ваш заказ был оформлен, " +
                $"в ближайшее время мы с вами свяжемся. Благодарим за заказ.";

            var smtpClient = new SmtpClient
            {
                Host = EmailConstants.SmtpHost,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            var message = new MailMessage()
            {
                From = fromAddress,
                Subject = subject,
                Body = body
            };

            message.To.Add(toAddress);

            await smtpClient.SendMailAsync(message);
        }
    }
}
