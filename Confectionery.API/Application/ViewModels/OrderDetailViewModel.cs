using Confectionery.Domain.Enums;

namespace Confectionery.API.Application.ViewModels
{
    public class OrderDetailViewModel : BaseViewModel
    {
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
