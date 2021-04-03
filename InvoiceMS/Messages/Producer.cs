using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceMS.Models.DTOs;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace InvoiceMS.Messages {
  public class Producer : IRmqProducer {
    private ConnectionFactory _rmqConnectionFactory = new ConnectionFactory() { HostName = "localhost" };
    private string _serviceQueueName = "InvoiceMS-events";

    public void PaymentSuccessful(long cartID) {
      using (IConnection rmqConnection = _rmqConnectionFactory.CreateConnection()) {
        using (IModel channel = rmqConnection.CreateModel()) {

          channel.QueueDeclare(
            queue: _serviceQueueName,
            durable: false,
            exclusive: false,
            autoDelete: false
            );

          var msg = new { EventType = "PaymentSuccessful", Payload = cartID };
          var msgBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));

          channel.BasicPublish(
            exchange: "",
            routingKey: _serviceQueueName,
            basicProperties: null,
            body: msgBody
            );

        }
      }
    }
  }
}
