using Confectionery.Domain.Enums;
using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.Entities
{
    public class Confection : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ConfectionType Type { get; set; }
        public decimal Weight { get; set; }
        public int MinimumOrderCount { get; set; }
        public bool IsOutOfStock { get; set; }

        public bool IsOrderCountLimited => MinimumOrderCount > 1;

        public List<ConfectionPicture> Pictures { get; set; }

    }
}
