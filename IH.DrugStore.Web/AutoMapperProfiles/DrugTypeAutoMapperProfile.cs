using AutoMapper;
using IH.DrugStore.Web.Data.Entities;
using IH.DrugStore.Web.Models.DrugTypes;

namespace IH.DrugStore.Web.AutoMapperProfiles
{
    public class DrugTypeAutoMapperProfile : Profile
    {
        public DrugTypeAutoMapperProfile()
        {
            CreateMap<DrugType, DrugTypeViewModel>().ReverseMap();
            CreateMap<DrugType, DrugTypeDetailsViewModel>();
        }
    }
}
