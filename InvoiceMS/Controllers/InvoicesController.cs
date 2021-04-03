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
  public class InvoicesController : ControllerBase {
    private IInvoiceService _invoiceService;
    public InvoicesController(IInvoiceService invoiceService) {
      _invoiceService = invoiceService;
    }

    // GET: api/v1/<InvoicesController>
    [HttpGet]
    public async Task<IEnumerable<InvoiceDTO>> Get() {
      return await _invoiceService.All();
    }

    // GET api/v1/<InvoicesController>/5

    //ID = userID 
    [HttpGet("{id}")]
    public async Task<InvoiceDTO> Get(int id) {
      return await _invoiceService.ByUserID(id);
    }


  }
}
