using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.Entities
{
    public class Confection : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public int MinimumOrderCount { get; set; }
        public bool isOutOfStock { get; set; }

        public ConfectionPicture Picture { get; set; }
    }
}
