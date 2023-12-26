namespace IH.DrugStore.Web.Data.Entities
{
    public class Drug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? BarCode { get; set; }

        public int DrugTypeId { get; set; }
        public DrugType DrugType { get; set; }

        public List<Order> Orders { get; set; }
    }
}
