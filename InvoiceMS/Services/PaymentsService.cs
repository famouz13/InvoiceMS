using AutoMapper;
using InvoiceMS.DAL;
using InvoiceMS.Mapper;
using InvoiceMS.Messages;
using InvoiceMS.Models.DTOs;
using InvoiceMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.Services {
  public class PaymentsService : IPaymentService {
    private IMapper _mapper;
    private IInvoicesRepository _invoicesRepository;
    private IPaymentRepository _paymentRepository;
    private IRmqProducer _rmqProducer;

    public PaymentsService(IInvoicesRepository invoicesRepository, IPaymentRepository paymentRepository, IRmqProducer rmqProducer) {
      _invoicesRepository = invoicesRepository;
      _paymentRepository = paymentRepository;
      _mapper = AutoMapperConfig.Configure().CreateMapper();
      _rmqProducer = rmqProducer;
    }

    public async Task<List<PaymentDTO>> All() {
      var items = await _paymentRepository.All();

      return _mapper.Map<List<PaymentDTO>>(items);
    }

    public async Task<PaymentDTO> ByID(int paymentID) {
      var payment = await _paymentRepository.ByID(paymentID);

      if (payment != null)
        return _mapper.Map<PaymentDTO>(payment);

      return new PaymentDTO();
    }

    public async Task<PaymentDTO> MakePayment(AddPaymentDTO addPaymentDTO) {
      var invoice = await _invoicesRepository.ByID(addPaymentDTO.InvoiceID);


      if (invoice != null && invoice?.Total == addPaymentDTO.PaymentAmount) {

        Payment invoicePayment = await _paymentRepository.ByInvoiceID(invoice.InvoiceID);
        //если оплата по счету уже сделана то повторно она не проводится
        if (invoicePayment != null)
          return _mapper.Map<PaymentDTO>(invoicePayment);

        Payment newPayment = new Payment() {
          InvoiceID = invoice.InvoiceID,
          PaymentAmount = invoice.Total,
          PaidAt = DateTime.Now
        };

        Payment addedPayment = await _paymentRepository.Add(newPayment);

        _rmqProducer.PaymentSuccessful(invoice.ShoppingCartID);

        return _mapper.Map<PaymentDTO>(addedPayment);
      }

      return new PaymentDTO();
    }

  }
}
