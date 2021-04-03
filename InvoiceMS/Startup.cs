using InvoiceMS.DAL;
using InvoiceMS.DataClients;
using InvoiceMS.Messages;
using InvoiceMS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static InvoiceMS.DAL._DAL;

namespace InvoiceMS {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {

      services.AddControllers();
      services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "InvoiceMS", Version = "v1" });
      });

      //data access layer
      services.AddSingleton<IPaymentRepository>(new Payments("InvoicesMSProd"));
      services.AddSingleton<IInvoicesRepository>(new Invoices("InvoicesMSProd"));


      //clients 
      services.AddSingleton<IShoppingCartMsClient, ShoppingCartMsClient>();

      //producers
      services.AddSingleton<IRmqProducer, Producer>();

      //service layer
      services.AddSingleton<IInvoiceService>(s =>
      new InvoicesService(
        s.GetRequiredService<IInvoicesRepository>(),
        s.GetRequiredService<IShoppingCartMsClient>())
      );

      services.AddSingleton<IPaymentService>(s =>
      new PaymentsService(
        s.GetRequiredService<IInvoicesRepository>(),
        s.GetRequiredService<IPaymentRepository>(),
        s.GetRequiredService<IRmqProducer>())
      );
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InvoiceMS v1"));
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
