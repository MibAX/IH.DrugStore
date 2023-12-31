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


        [ValidateNever]
        public SelectList CustomerSelectList { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }



        [ValidateNever]
        public MultiSelectList DrugMultiSelectList { get; set; }

        [Display(Name = "Drugs")]
        public List<int> DrugIds { get; set; }
    }
}
