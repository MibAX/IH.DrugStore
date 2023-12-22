namespace IH.DrugStore.Web.Data.Entities
{
    public class DrugType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Drug> Drugs { get; set; }
    }
}
