using InvoiceMS.Models.DTOs.External.ShoppingCartMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.DataClients {
  public class ShoppingCartMsClientMock : IShoppingCartMsClient {

    public List<ShoppingCartDTO> ShoppingCartDTOs = new List<ShoppingCartDTO>() {
      new ShoppingCartDTO() {
        ShoppingCartID=1,
        UserID=1,
        CreatedAt=DateTime.Now,
        Expires=DateTime.Now.AddDays(10),
        Items= new List<ShoppingCartItemDTO>() {
          new ShoppingCartItemDTO() {
            ShoppingCartItemID=1,
            CommodityDetails="Pepsi шт.",
            CommodityID=1,
            CommodityPrice=10,
            Quantity=20
          },
          new ShoppingCartItemDTO() {
            ShoppingCartItemID=1,
            CommodityDetails="Fanta шт.",
            CommodityID=1,
            CommodityPrice=9,
            Quantity=15
          }
        },
      }
    };
    public async Task<ShoppingCartDTO> GetShoppingCartByUserID(int userID) {
      return ShoppingCartDTOs.FirstOrDefault(n => n.UserID == userID);
    }
  }
}
