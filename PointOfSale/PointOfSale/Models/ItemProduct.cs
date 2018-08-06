using System;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models
{
    public class ItemProduct
    {
        [Key]
        public int ItemProductId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Required]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}