using AutoMapper;
using InvoiceMS.Models.DTOs;
using InvoiceMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.Mapper {
  public class AutoMapperConfig {
    public static MapperConfiguration Configure() {
      var config = new MapperConfiguration(cfg => {

        cfg.CreateMap<Invoice, InvoiceDTO>().ReverseMap();

        cfg.CreateMap<Payment, PaymentDTO>().ReverseMap();

      });

      return config;
    }
  }
}
