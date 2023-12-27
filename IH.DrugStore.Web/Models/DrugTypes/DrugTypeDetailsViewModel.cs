using IH.DrugStore.Web.Models.Drugs;

namespace IH.DrugStore.Web.Models.DrugTypes
{
    public class DrugTypeDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<DrugViewModel> Drugs { get; set; }
    }
}
