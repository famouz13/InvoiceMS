using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.Models.DTOs {
  public class InvoiceDTO {
    public int InvoiceID { get; set; }
    public int UserID { get; set; }
    public int ShoppingCartID { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CountOfCommodities { get; set; }
    public decimal Total { get; set; }
  }
}
