using Confectionery.API.Application.ViewModels;
using Confectionery.Domain.Entities;
using Mapster;

namespace Confectionery.API.Mappings
{
    public class MapsterEntityModelConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Confection, ConfectionViewModel>()
                .Map(d => d.Id, s => s.Id)
                .Map(d => d.Name, s => s.Name)
                .Map(d => d.Description, s => s.Description)
                .Map(d => d.Price, d => d.Price)
                .Map(d => d.Weight, d => d.Weight)
                .Map(d => d.IsOrderCountLimited, d => d.IsOrderCountLimited)
                .Map(d => d.MinimumOrderCount, d => d.MinimumOrderCount)
                .Map(d => d.IsOutOfStock, d => d.IsOutOfStock);

            config.NewConfig<ConfectionPicture, ConfectionPictureViewModel>()
                .Map(d => d.Id, s => s.Id)
                .Map(d => d.ShortName, s => s.ShortName)
                .Map(d => d.Extension, s => s.Extension)
                .Map(d => d.Content, d => d.Content);
        }
    }
}
