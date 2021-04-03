using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceMS.Models.Entities {
  public class Payment {
    [Key]
    public int PaymentID { get; set; }


    [ForeignKey("Invoice")]
    public int InvoiceID { get; set; }

    [Required]
    public virtual Invoice Invoice { get; set; }

    [Required]
    public DateTime PaidAt { get; set; }

    [Required]
    public double PaymentAmount { get; set; }

  }
}
