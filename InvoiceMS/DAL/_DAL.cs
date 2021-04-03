using InvoiceMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvoiceMS.Models.Entities;

namespace InvoiceMS.DAL {
  public static class _DAL {
    public class Invoices : IInvoicesRepository {

      private string _dbName;
      public Invoices(string dbName) {
        _dbName = dbName;
      }

      public async Task<Invoice> ByID(int invoiceID) {
        using (var db = new InvoiceMsDbContext(_dbName)) {
          return await db.Invoices.FirstOrDefaultAsync(n => n.InvoiceID == invoiceID);
        }
      }
      public async Task<List<Invoice>> All() {
        using (var db = new InvoiceMsDbContext(_dbName)) {
          return await db.Invoices.AsNoTracking().ToListAsync();
        }
      }

      public async Task<Invoice> ByShoppingCartID(long cartID) {
        using (var db = new InvoiceMsDbContext(_dbName)) {
          return await db.Invoices.AsNoTracking().FirstOrDefaultAsync(n => n.ShoppingCartID == cartID);
        }
      }

      public async Task<Invoice> Add(Invoice newInvoice) {
        using (var db = new InvoiceMsDbContext(_dbName)) {
          await db.AddAsync(newInvoice);

          await db.SaveChangesAsync();
          return newInvoice;
        }
      }

      public async Task<bool> DeleteByID(int invoiceID) {
        using (var db = new InvoiceMsDbContext(_dbName)) {
          var itemToDelete = await db.Invoices.FirstOrDefaultAsync(n => n.InvoiceID == invoiceID);

          if (itemToDelete == null)
            return false;

          db.Invoices.Remove(itemToDelete);
          await db.SaveChangesAsync();
          return true;
        }
      }

    }

    public class Payments : IPaymentRepository {

      private string _dbName;
      public Payments(string dbName) {
        _dbName = dbName;
      }
      public async Task<List<Payment>> All() {
        using (var db = new InvoiceMsDbContext(_dbName)) {
          return await db.Payments.AsNoTracking().ToListAsync();
        }
      }

      public async Task<Payment> ByID(int paymentID) {
        using (var db = new InvoiceMsDbContext(_dbName)) {
          return await db.Payments.FirstOrDefaultAsync(n => n.PaymentID == paymentID);
        }
      }

      public async Task<Payment> ByInvoiceID(int invoiceID) {
        using (var db = new InvoiceMsDbContext(_dbName)) {
          return await db.Payments.FirstOrDefaultAsync(n => n.InvoiceID == invoiceID);
        }
      }

      public async Task<Payment> Add(Payment newPayment) {
        using (var db = new InvoiceMsDbContext(_dbName)) {
          await db.AddAsync(newPayment);

          await db.SaveChangesAsync();

          return newPayment;
        }
      }
    }
  }
}
