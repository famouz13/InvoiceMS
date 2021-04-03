using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using InvoiceMS.Services;
using InvoiceMS.DAL;
using static InvoiceMS.DAL._DAL;
using System.Threading.Tasks;
using InvoiceMS.Models.DTOs;
using System;
using InvoiceMS.Models.Entities;
using InvoiceMS.DataClients;
using InvoiceMS.Models.DTOs.External.ShoppingCartMS;

namespace InvoiceMsTests {
  [TestClass]
  public class InvoiceServiceTests {
    private ServiceProvider _serviceProvider;
    private IInvoiceService _invoicesService;
    private IInvoicesRepository _invoicesRepository;
    private IShoppingCartMsClient _shoppingCartMsClient;
    public InvoiceServiceTests() {
      _serviceProvider = new ServiceCollection()
        .AddSingleton<IInvoicesRepository>(new Invoices("InvoicesMSDev")) //тестовая бд
        .AddSingleton<IShoppingCartMsClient, ShoppingCartMsClientMock>()
        .AddSingleton<IInvoiceService>(s => new InvoicesService(s.GetRequiredService<IInvoicesRepository>(), s.GetRequiredService<IShoppingCartMsClient>()))
        .BuildServiceProvider();

      _invoicesRepository = _serviceProvider.GetRequiredService<IInvoicesRepository>();
      _invoicesService = _serviceProvider.GetRequiredService<IInvoiceService>();
      _shoppingCartMsClient = _serviceProvider.GetRequiredService<IShoppingCartMsClient>();
    }


    [TestMethod]
    public async Task NewInvoice_IfCartDoesntExists() {

      InvoiceDTO invoiceDTO = await _invoicesService.ByUserID(2);

      //проверяем что счет на несуществующую корзину не был создан
      Assert.IsTrue(invoiceDTO.InvoiceID == 0);
    }

    [TestMethod]
    public async Task RewriteInvoice_IfCartInfoChanged() {
      Invoice invoice = await _invoicesRepository.ByShoppingCartID(1);

      var rand = new Random();

      var client = (ShoppingCartMsClientMock)_shoppingCartMsClient;

      client.ShoppingCartDTOs[0].Items.Add(new ShoppingCartItemDTO() {
        CommodityDetails = "asdasd",
        ShoppingCartItemID = rand.Next(100),
        CommodityID = rand.Next(100),
        CommodityPrice = rand.Next(100),
        Quantity = rand.Next(10)
      });

      InvoiceDTO invoiceDTO = await _invoicesService.ByUserID(1);

      //проверяем пересоздался ли счет
      Assert.AreNotEqual(invoice.InvoiceID, invoiceDTO.InvoiceID);
    }

    [TestMethod]
    public async Task NewInvoice_IfCartExists() {

      //проверяем создание нового счета
      InvoiceDTO invoiceDTO = await _invoicesService.ByUserID(1);

      Assert.IsTrue(invoiceDTO.InvoiceID > 0);
      Assert.AreNotEqual(DateTime.MinValue, invoiceDTO.CreatedAt);
      Assert.IsTrue(invoiceDTO.CountOfCommodities > 0);
      Assert.IsTrue(invoiceDTO.Total > 0);
    }
  }
}
