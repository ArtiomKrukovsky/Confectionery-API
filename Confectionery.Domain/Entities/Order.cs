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

        public Order(Guid confectionId, decimal unitPrice, int quentity)
        {
            ConfectionId = confectionId;
            UnitPrice = unitPrice;
            Quentity = quentity;

            CreatedDtm = DateTime.UtcNow;
        }

        public void SetUser(User user)
        {
            User = user;
        }
    }
}
