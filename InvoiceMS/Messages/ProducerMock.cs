using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.Messages {
  public class ProducerMock : IRmqProducer {
    public void PaymentSuccessful(long cartID) {

    }
  }
}
