using Microsoft.Extensions.Hosting;
using MongoDB.Bson.IO;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace DataService.RabbitMQ
{
    public class BackgroundSubscriber : IHostedService
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private string queueName;
        private EventingBasicConsumer consumer;

        public void InitBackgroundDataSubscriber()
        {
            //ovo sto je zakomentarisano je za docker
            this._factory = new ConnectionFactory() { HostName = "localhost" }; //{ HostName = "rabbitmq", Port = 5672  };
            // this._factory.UserName = "guest";
            //this._factory.Password = "guest";
            this._connection = this._factory.CreateConnection();
            this._channel = this._connection.CreateModel();
            this._channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

            var queueName = this._channel.QueueDeclare().QueueName;
            this._channel.QueueBind(queue: queueName,
                              exchange: "logs",
                              routingKey: "");

            var consumer = new EventingBasicConsumer(this._channel);
            consumer.Received += (model, ea) =>
            {
                //var body = ea.Body;
                //var message = Encoding.UTF8.GetString(body.ToArray());
                //ovo je nacin da se primi OBJEKAT, obj se stavlja u message, za sada se samo stampa
                //ovde treba da se ubaci upisivanje u bazu jeje
                var json = Encoding.Default.GetString(ea.Body.ToArray());
                var message = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                Console.WriteLine(" [x] {0}", message);
            };
            this._channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("SAMO TEST DA LI SE NESTO DESAVA");
            InitBackgroundDataSubscriber();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._channel.Dispose();
            this._connection.Dispose();
            return Task.CompletedTask;
        }
    }
}
