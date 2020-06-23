using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.RabbitMQ
{
    public class Publisher : IPublisher
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public Publisher()
        {
            //zakoemntarisano je za docker
            //this._factory = new ConnectionFactory();// { HostName = "localhost" };
            this._factory = new ConnectionFactory()
            {
                 HostName = "rabbitmq",
                UserName = "user",
                Password = "password",
                Port = 5672
            };
            this._connection = _factory.CreateConnection();
            this._channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "data-analytics", type: ExchangeType.Fanout);
            Console.WriteLine(" Press [enter] to exit.");
        }

        public void SendMessage(object o)
        {
            //var body = Encoding.UTF8.GetBytes(test);
            var body = ObjectSerialize.Serialize(o);
            this._channel.BasicPublish(exchange: "data-analytics",
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
