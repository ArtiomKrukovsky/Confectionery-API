using Confectionery.Domain.Enums;
using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.Entities
{
    public class Order : Entity
    {
        public Guid ConfectionId { get; set; }
        public Guid ClientId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quentity { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDtm { get; set; }

        public Client Client { get; set; }
        public Confection Confection { get; set; }

        public Order(Guid confectionId, Guid clientId, decimal unitPrice, int quentity)
        {
            ConfectionId = confectionId;
            ClientId = clientId;
            UnitPrice = unitPrice;
            Quentity = quentity;

            Status = OrderStatus.Submitted;
            CreatedDtm = DateTime.UtcNow;
        }

        public void SetCancelledStatus()
        {
            if (Status == OrderStatus.Paid ||
                Status == OrderStatus.Shipping ||
                Status == OrderStatus.Cooking)
            {
                StatusChangeException(OrderStatus.Cancelled);
            }

            Status = OrderStatus.Cancelled;
        }

        private void StatusChangeException(OrderStatus orderStatusToChange)
        {
            throw new ArgumentException($"Is not possible to change the order status from {nameof(Status)} " +
                $"to {nameof(orderStatusToChange)}.");
        }
    }
}
