using AutoMapper;
using IH.DrugStore.Web.Data.Entities;
using IH.DrugStore.Web.Models.Drugs;

namespace IH.DrugStore.Web.AutoMapperProfiles
{
    public class DrugAutoMapperProfile : Profile
    {
        public DrugAutoMapperProfile()
        {
            CreateMap<Drug, DrugListViewModel>();
            CreateMap<Drug, DrugDetailsViewModel>();

            CreateMap<CreateUpdateDrugViewModel, Drug>().ReverseMap();
        }
    }
}
