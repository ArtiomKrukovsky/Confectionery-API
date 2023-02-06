using Confectionery.API.Application.Interfaces;
using Confectionery.Domain.IRepositories;
using FluentValidation;
using MediatR;
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

        }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
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
            await SendEmailAsync(user.Email);

            return true;
        }

        private async Task SendEmailAsync(string userEmail)
        {
            var fromAddress = new MailAddress("from@gmail.com", "From");
            var toAddress = new MailAddress(userEmail, "To");

            const string fromPassword = "fromPassword";
            const string subject = "Ваш заказ был оформлен!";
            const string body = "Заказ был оформлен";

            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
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
