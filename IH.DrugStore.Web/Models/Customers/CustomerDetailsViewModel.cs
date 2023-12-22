using IH.DrugStore.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace IH.DrugStore.Web.Models.Customers
{
    public class CustomerDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public int Age { get; set; }

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        public Gender Gender { get; set; }

        // TO DO add List<OrderListViewModel>
    }
}
