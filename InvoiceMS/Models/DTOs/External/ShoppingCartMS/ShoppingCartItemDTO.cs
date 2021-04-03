using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.Models.DTOs.External.ShoppingCartMS {
  public class ShoppingCartItemDTO {
    public long ShoppingCartItemID { get; set; }

    public int CommodityID { get; set; }

    public string CommodityDetails { get; set; }

    public decimal CommodityPrice { get; set; }

    public double Quantity { get; set; }

    public double Sum {
      get {
        return (double)CommodityPrice * Quantity;
      }
    }
  }
}
