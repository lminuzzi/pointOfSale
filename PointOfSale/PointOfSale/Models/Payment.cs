using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Payment")]
        public string Name { get; set; }
    }
}