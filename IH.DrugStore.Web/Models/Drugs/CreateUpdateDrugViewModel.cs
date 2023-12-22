using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IH.DrugStore.Web.Models.Drugs
{
    public class CreateUpdateDrugViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        [Display(Name = "Bar Code")]
        public string? BarCode { get; set; }


        [Display(Name = "Drug Type")]
        public int DrugTypeId { get; set; }


        [ValidateNever]
        public SelectList DrugTypeSelectList { get; set; }
    }
}
