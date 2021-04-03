using InvoiceMS.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.Services {
  public interface IInvoiceService {
    Task<List<InvoiceDTO>> All();
    Task<InvoiceDTO> ByUserID(int userID);

  }
}
