using Confectionery.API.Application.ViewModels;
using Confectionery.API.Application.ViewModels.Common;
using Confectionery.Domain.Entities;
using Confectionery.Infrastructure.QueryProcessing;
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
                .Map(d => d.ConfectionType, d => d.Type)
                .Map(d => d.IsOrderCountLimited, d => d.IsOrderCountLimited)
                .Map(d => d.MinimumOrderCount, d => d.MinimumOrderCount)
                .Map(d => d.IsOutOfStock, d => d.IsOutOfStock)
                .Map(d => d.Pictures, d => d.Pictures);

            config.NewConfig<ConfectionPicture, ConfectionPictureViewModel>()
                .Map(d => d.Id, s => s.Id)
                .Map(d => d.ShortName, s => s.ShortName)
                .Map(d => d.Url, d => d.Url);

            config.NewConfig<Client, ClientViewModel>()
                .Map(d => d.Id, s => s.Id)
                .Map(d => d.FullName, s => s.FullName)
                .Map(d => d.Email, d => d.Email)
                .Map(d => d.InstagramProfile, d => d.InstagramProfile)
                .Map(d => d.MobileNumber, d => d.MobileNumber);

            config.NewConfig<Order, OrderDetailViewModel>()
                .Map(d => d.Id, s => s.Id)
                .Map(d => d.CustomerName, s => s.Client.FullName)
                .Map(d => d.ProductName, d => d.Confection.Name)
                .Map(d => d.Status, d => d.Status)
                .Map(d => d.CreatedDate, d => d.CreatedDtm)
                .Map(d => d.UnitPrice, d => d.UnitPrice)
                .Map(d => d.Quantity, d => d.Quentity);

            config.NewConfig<PagedList<Order>, PagedListViewModel<OrderDetailViewModel>>()
                .Map(d => d.Items, s => s.Items)
                .Map(d => d.CurrentPage, s => s.CurrentPage)
                .Map(d => d.TotalPages, d => d.TotalPages)
                .Map(d => d.PageSize, d => d.PageSize)
                .Map(d => d.TotalCount, d => d.TotalCount);

            config.NewConfig<PagedList<Confection>, PagedListViewModel<ConfectionViewModel>>()
                .Map(d => d.Items, s => s.Items)
                .Map(d => d.CurrentPage, s => s.CurrentPage)
                .Map(d => d.TotalPages, d => d.TotalPages)
                .Map(d => d.PageSize, d => d.PageSize)
                .Map(d => d.TotalCount, d => d.TotalCount);
        }
    }
}
