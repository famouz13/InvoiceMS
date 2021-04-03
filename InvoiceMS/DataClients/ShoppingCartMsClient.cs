using InvoiceMS.Models.DTOs.External.ShoppingCartMS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InvoiceMS.DataClients {
  public class ShoppingCartMsClient : IShoppingCartMsClient {
    private string shoppingCartMsEndPoint = "http://localhost:63825";
    private string apiBaseAddress = "api/v1/Cart";
    private HttpClient httpClient = new HttpClient();

    public async Task<ShoppingCartDTO> GetShoppingCartByUserID(int userID) {
      string requestURI = $"{shoppingCartMsEndPoint}/{apiBaseAddress}/{userID}";

      string cartJson = await httpClient.GetStringAsync(requestURI);

      ShoppingCartDTO cartDTO = JsonConvert.DeserializeObject<ShoppingCartDTO>(cartJson);

      return cartDTO;
    }
  }
}
