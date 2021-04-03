using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using InvoiceMS.Models.Entities;
using InvoiceMS.Config;

namespace InvoiceMS.DB {
  public class InvoiceMsDbContext : DbContext {
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Payment> Payments { get; set; }

    private string _dbName;

    public InvoiceMsDbContext(string dbName) {
      _dbName = dbName;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      string dbConnection = _Cfg.GetConnectionString(_dbName);

      optionsBuilder.UseSqlServer(dbConnection);
    }
  }
}
