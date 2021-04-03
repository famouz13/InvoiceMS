using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.Config {
  public static class _Cfg {
    private static IConfiguration appConfig = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json")
      .Build();

    public static string GetConnectionString(string dbName) {
      return appConfig.GetConnectionString(dbName);
    }


  }
}
