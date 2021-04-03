using InvoiceMS.Models.DTOs;
using InvoiceMS.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceMS.Controllers {
  [Route("api/v1/[controller]")]
  [ApiController]
  public class PaymentsController : ControllerBase {
    // GET: api/v1/<PaymentsController>

    private IPaymentService _paymentService;
    public PaymentsController(IPaymentService paymentService) {
      _paymentService = paymentService;
    }

    [HttpGet]
    public async Task<IEnumerable<PaymentDTO>> Get() {
      return await _paymentService.All();
    }

    // GET api/v1/<PaymentsController>/5
    [HttpGet("{id}")]
    public async Task<PaymentDTO> Get(int id) {
      return await _paymentService.ByID(id);
    }

    // POST api/v1/<PaymentsController>
    [HttpPost]
    public async Task<PaymentDTO> Post([FromBody] AddPaymentDTO addPaymentDTO) {
      return await _paymentService.MakePayment(addPaymentDTO);
    }


  }
}
