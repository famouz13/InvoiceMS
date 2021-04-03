using InvoiceMS.DAL;
using InvoiceMS.Messages;
using InvoiceMS.Models.DTOs;
using InvoiceMS.Models.Entities;
using InvoiceMS.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InvoiceMS.DAL._DAL;

namespace InvoiceMsTests {
  [TestClass]
  public class PaymentsServiceTests {
    private ServiceProvider _serviceProvider;
    private IPaymentService _paymentService;
    private IInvoicesRepository _invoicesRepository;
    private IPaymentRepository _paymentRepository;
    public PaymentsServiceTests() {
      _serviceProvider = new ServiceCollection()
        .AddSingleton<IInvoicesRepository>(new Invoices("InvoicesMSDev")) //тестовая бд
        .AddSingleton<IPaymentRepository>(new Payments("InvoicesMSDev"))
        .AddSingleton<IRmqProducer, ProducerMock>() //чтобы не использовать шину сообщений
        .AddSingleton<IPaymentService>(s =>
        new PaymentsService(
          s.GetRequiredService<IInvoicesRepository>(),
          s.GetRequiredService<IPaymentRepository>(),
          s.GetRequiredService<IRmqProducer>())
        )
        .BuildServiceProvider();

      _invoicesRepository = _serviceProvider.GetRequiredService<IInvoicesRepository>();
      _paymentRepository = _serviceProvider.GetRequiredService<IPaymentRepository>();
      _paymentService = _serviceProvider.GetRequiredService<IPaymentService>();
    }

    [TestMethod]
    public async Task IfDoublePayment() {
      Invoice newInvoice = new Invoice() {
        ShoppingCartID = 2,
        UserID = 2,
        CountOfCommodities = 3,
        CreatedAt = DateTime.Now,
        Total = 1500
      };

      Invoice addedInvoice = await _invoicesRepository.Add(newInvoice);

      AddPaymentDTO addPaymentDTO = new AddPaymentDTO() { InvoiceID = addedInvoice.InvoiceID, PaymentAmount = addedInvoice.Total };

      PaymentDTO firstPayment = await _paymentService.MakePayment(addPaymentDTO);
      PaymentDTO secondPayment = await _paymentService.MakePayment(addPaymentDTO);

      //На повторную оплату должен вернуть уже существующую по счету
      Assert.AreEqual(firstPayment.PaymentID, secondPayment.PaymentID);
    }
    [TestMethod]
    public async Task IfInvoiceDoesntExists() {
      AddPaymentDTO addPaymentDTO = new AddPaymentDTO() { InvoiceID = -1, PaymentAmount = 0 };
      PaymentDTO paymentDTO = await _paymentService.MakePayment(addPaymentDTO);
      //оплата по несуществующему счету должна не пройти
      Assert.AreEqual(0, paymentDTO.PaymentID);
    }
    [TestMethod]
    public async Task PaymentIntegrity() {
      var invoice = await _invoicesRepository.ByShoppingCartID(1);

      AddPaymentDTO addPaymentDTO = new AddPaymentDTO() {
        InvoiceID = invoice.InvoiceID,
        PaymentAmount = invoice.Total
      };

      PaymentDTO paymentDTO = await _paymentService.MakePayment(addPaymentDTO);

      Assert.IsTrue(paymentDTO.PaymentID > 0);
      Assert.AreNotEqual(DateTime.MinValue, paymentDTO.PaidAt);
      Assert.IsTrue(Convert.ToDouble(paymentDTO.PaymentAmount) == addPaymentDTO.PaymentAmount);
    }

  }
}
