using IH.DrugStore.Web.Enums;

namespace IH.DrugStore.Web.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public double TotalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }


        public int CustomerId { get; set; }
        public Customer Customer { get; set; }


        public List<Drug> Drugs { get; set; } = new List<Drug>();
    }
}
