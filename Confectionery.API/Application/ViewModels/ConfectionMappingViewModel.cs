using Confectionery.Domain.Enums;

namespace Confectionery.API.Application.ViewModels
{
    public class ConfectionMappingViewModel
    {
        public ConfectionType ConfectionType { get; set; }
        public List<ConfectionViewModel> Confections { get; set; }
    }
}
