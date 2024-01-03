using AutoMapper;
using IH.DrugStore.Web.Data.Entities;
using IH.DrugStore.Web.Models.Orders;

namespace IH.DrugStore.Web.AutoMapperProfiles
{
    public class OrderAutoMapperProfile : Profile
    {
        public OrderAutoMapperProfile()
        {
            CreateMap<CreateUpdateOrderViewModel, Order>().ReverseMap();
            CreateMap<Order, OrderViewModel>();
        }
    }
}
