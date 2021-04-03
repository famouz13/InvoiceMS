using InvoiceMS.Models.DTOs.External.ShoppingCartMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.DataClients {
  public interface IShoppingCartMsClient {
    Task<ShoppingCartDTO> GetShoppingCartByUserID(int userID);
  }
}
