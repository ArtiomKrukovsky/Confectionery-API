namespace Confectionery.API.Application.ViewModels
{
    public class ConfectionViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public bool IsOrderCountLimited { get; set; }
        public int MinimumOrderCount { get; set; }
        public bool IsOutOfStock { get; set; }

        public List<ConfectionPictureViewModel> Pictures { get; set; }
    }
}
