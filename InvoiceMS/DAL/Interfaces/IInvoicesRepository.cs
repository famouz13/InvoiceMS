using InvoiceMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.DAL {
  public interface IInvoicesRepository {
    Task<Invoice> ByID(int invoiceID);

    Task<List<Invoice>> All();

    Task<Invoice> ByShoppingCartID(long cartID);

    Task<Invoice> Add(Invoice newInvoice);

    Task<bool> DeleteByID(int invoiceID);

  }
}
