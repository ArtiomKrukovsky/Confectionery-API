using Confectionery.API.Application.Interfaces;
using Confectionery.Domain.IRepositories;
using FluentValidation;
using MediatR;
using System.Net;
using System.Net.Mail;

namespace Confectionery.API.Application.Queries.Order
{
    public class CreateOrderCommand : ICommand<bool>
    {
        public Guid ConfectionId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string InstagramProfile { get; set; }
        public string MobileNumber { get; set; }

        public CreateOrderCommand(
            Guid confectionId,
            decimal unitPrice,
            int quantity,
            string fullName,
            string email,
            string instagramProfile,
            string mobileNumber)
        {
            ConfectionId = confectionId;
            UnitPrice = unitPrice;
            Quantity = quantity;

            FullName = fullName;
            Email = email;
            InstagramProfile = instagramProfile;
            MobileNumber = mobileNumber;
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
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user is null)
            {
                user = new Domain.Entities.User(
                    request.FullName, 
                    request.Email, 
                    request.InstagramProfile, 
                    request.MobileNumber); 
            }

            var order = new Domain.Entities.Order(
                request.ConfectionId,
                request.UnitPrice,
                request.Quantity);

            // todo: use domain events instead
            order.SetUser(user);

            await _orderRepository.CreateAsync(order);
            await _userRepository.SaveChangesAsync(cancellationToken);

            await SendEmailAsync(request.Email);

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
