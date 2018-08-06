using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace PointOfSale.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Category")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}