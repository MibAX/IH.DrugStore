using IH.DrugStore.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace IH.DrugStore.Web.Models.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Order Time")]
        public DateTime OrderTime { get; set; }

        [Display(Name = "Total Price")]
        public double TotalPrice { get; set; }

        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "Customer")]
        public string CustomerFullName { get; set; }
    }
}
