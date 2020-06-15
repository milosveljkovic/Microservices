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
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public Publisher()
        {
            //zakoemntarisano je za docker
            this._factory = new ConnectionFactory();// { HostName = "localhost" };
            //this._factory = new ConnectionFactory()
            //{
           //     HostName = "rabbitmq",
            //    UserName = "user",
            //    Password = "password",
            //    Port = 5672
            //};
            this._connection = _factory.CreateConnection();
            this._channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
            Console.WriteLine(" Press [enter] to exit.");
        }

        public void SendMessage(object o)
        {
            //var body = Encoding.UTF8.GetBytes(test);
            var body = ObjectSerialize.Serialize(o); 
            this._channel.BasicPublish(exchange: "logs",
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
