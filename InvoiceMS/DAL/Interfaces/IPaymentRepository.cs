using InvoiceMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.DAL {
  public interface IPaymentRepository {
    Task<List<Payment>> All();


    Task<Payment> ByID(int paymentID);


    Task<Payment> ByInvoiceID(int invoiceID);

    Task<Payment> Add(Payment newPayment);
  }
}
