using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public String UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public DateTime DtOrder { get; set; }
        public OrderStatus OrderStatus { get; set; }
        [Display(Name = "PayDay")]
        public DateTime DtPayment { get; set; }
        [Required]
        [Display(Name = "Amount")]
        public Decimal Amount { get; set; }
        [Required]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        public virtual List<ItemProduct> ItemProduct { get; set; }
    }
}