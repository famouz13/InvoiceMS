using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace InvoiceMS.Models.Entities {
  public class Invoice {
    [Key]
    public int InvoiceID { get; set; }

    [Required]
    public int UserID { get; set; }

    [Required]
    public long ShoppingCartID { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public int CountOfCommodities { get; set; }

    [Required]
    public double Total { get; set; }
  }
}
