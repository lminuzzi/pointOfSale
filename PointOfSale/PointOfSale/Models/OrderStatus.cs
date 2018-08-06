using System;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models
{
    public enum OrderStatus
    {
        //It means the payment has already been completed and credited, and the items were delivered.
        [Display(Name = "Complete")]
        Complete = 1,
        //Payment has already been processed and approved.
        [Display(Name = "Approved")]
        Approved = 2,
        //Payment has been initiated but is being reviewed by PagSeguro.
        [Display(Name = "InAnalysis")]
        InAnalysis = 3,
        //Payment was returned.
        [Display(Name = "Returned")]
        Returned = 4,
        //The transaction has been canceled.
        [Display(Name = "Canceled")]
        Canceled = 5
    }
}