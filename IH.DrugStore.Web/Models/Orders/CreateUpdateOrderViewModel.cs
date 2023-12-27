using IH.DrugStore.Web.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IH.DrugStore.Web.Models.Orders
{
    public class CreateUpdateOrderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }


        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        public List<int> DrugIds { get; set; }





        [ValidateNever]
        public SelectList Customer { get; set; }
    }
}
