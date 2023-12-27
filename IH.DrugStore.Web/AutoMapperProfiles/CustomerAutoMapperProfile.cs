using AutoMapper;
using IH.DrugStore.Web.Data.Entities;
using IH.DrugStore.Web.Models.Customers;

namespace IH.DrugStore.Web.AutoMapperProfiles
{
    public class CustomerAutoMapperProfile : Profile
    {
        public CustomerAutoMapperProfile()
        {
            CreateMap<Customer, CustomeViewModel>();
            CreateMap<Customer, CustomerDetailsViewModel>();
            CreateMap<CreateUpdateCustomerViewModel, Customer>().ReverseMap();
        }
    }
}
