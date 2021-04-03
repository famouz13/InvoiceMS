using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.Models.DTOs {
  public class PaymentDTO {
    public int PaymentID { get; set; }
    public int InvoiceID { get; set; }
    public DateTime PaidAt { get; set; }
    public decimal PaymentAmount { get; set; }

  }
}
