using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.Messages {
  public interface IRmqProducer {
    void PaymentSuccessful(long cartID);
  }
}
