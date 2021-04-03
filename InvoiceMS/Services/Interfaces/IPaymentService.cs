using InvoiceMS.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.Services {
  public interface IPaymentService {
    Task<List<PaymentDTO>> All();
    Task<PaymentDTO> ByID(int paymentID);
    Task<PaymentDTO> MakePayment(AddPaymentDTO addPaymentDTO);
  }
}
