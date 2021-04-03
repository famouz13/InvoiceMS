using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.Models.DTOs {
  public class AddPaymentDTO {
    public int InvoiceID { get; set; }

    public double PaymentAmount { get; set; }
  }
}
