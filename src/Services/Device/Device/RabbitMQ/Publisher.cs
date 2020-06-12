using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Device.RabbitMQ
{
    public class Publisher : IPublisher
    {
        private  ConnectionFactory _factory;
        private  IConnection _connection;
        private  IModel _channel;
        private  string queueName;
        private  EventingBasicConsumer consumer;

        Publisher()
        {
           this._factory = new ConnectionFactory() { HostName = "localhost" };
           this._connection = _factory.CreateConnection();
            this._channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
                //Console.WriteLine(" [x] Sent {0}", message);
            

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        public void SendMessage(string test)
        {
            var body = Encoding.UTF8.GetBytes(test);
            this._channel.BasicPublish(exchange: "logs",
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
