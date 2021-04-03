using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.Models.DTOs.External.ShoppingCartMS {
  public class ShoppingCartDTO {
    public long ShoppingCartID { get; set; }

    public int UserID { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime Expires { get; set; }

    public List<ShoppingCartItemDTO> Items { get; set; }

    public double Total {
      get {
        return Items.Sum(i => i.Sum);
      }
    }
  }
}
