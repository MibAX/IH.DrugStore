using IH.DrugStore.Web.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace IH.DrugStore.Web.Models.Customers
{
    public class CreateUpdateCustomerViewModel
    {
        public int Id { get; set; }


        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }


        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        public Gender Gender { get; set; }


        [Display(Name = "Full Name")]
        [ValidateNever]
        public string FullName { get; set; }
    }
}
