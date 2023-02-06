using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.Entities
{
    public class Order : Entity
    {
        public Guid ConfectionId { get; set; }
        public Guid UserId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quentity { get; set; }
        public DateTime CreatedDtm { get; set; }

        public User User { get; set; }
        public Confection Confection { get; set; }

        public Order(Guid confectionId, Guid userId, decimal unitPrice, int quentity)
        {
            ConfectionId = confectionId;
            UserId = userId;
            UnitPrice = unitPrice;
            Quentity = quentity;

            CreatedDtm = DateTime.UtcNow;
        }
    }
}
