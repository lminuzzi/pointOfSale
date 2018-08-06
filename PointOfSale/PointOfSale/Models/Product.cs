using System;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Product")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 999999.99)]
        public Decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}