using Analytics.Entities;
using Analytics.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Analytics.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Analytics.RabbitMQ
{
    public class BackgroundSubscriber : IHostedService
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private string queueName;
        private EventingBasicConsumer consumer;
        private readonly ISensorRepository _repository;
        private readonly ILogger<BackgroundSubscriber> _logger;
        private IHubContext<MessageHub> _messageHubContext;
        //string urlController = "http://localhost:5004/api/command/sendNotification";
        
        //docker
        string urlController = "http://172.17.0.1:5004/api/command/sendNotification";


        public BackgroundSubscriber(ISensorRepository repository, ILogger<BackgroundSubscriber> logger, IHubContext<MessageHub> messageHubContext)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _messageHubContext = messageHubContext ?? throw new ArgumentNullException(nameof(messageHubContext));
        }

        public void InitBackgroundDataSubscriber()
        {
            //this._factory = new ConnectionFactory() { HostName = "localhost" }; //{ HostName = "rabbitmq", Port = 5672  };
            this._factory = new ConnectionFactory()
            {
                 HostName = "rabbitmq",
                 UserName = "user",
                 Password = "password",
                 Port = 5672
              };
            this._connection = this._factory.CreateConnection();
            this._channel = this._connection.CreateModel();
            this._channel.ExchangeDeclare(exchange: "data-analytics", type: ExchangeType.Fanout);

            var queueName = this._channel.QueueDeclare().QueueName;
            this._channel.QueueBind(queue: queueName,
                              exchange: "data-analytics",
                              routingKey: "");

            var consumer = new EventingBasicConsumer(this._channel);
            consumer.Received += (model, ea) =>
            {
                var json = Encoding.Default.GetString(ea.Body.ToArray());
                var message = Newtonsoft.Json.JsonConvert.DeserializeObject<Sensor>(json);
                filterData(message);
            };
            this._channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            InitBackgroundDataSubscriber();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._channel.Dispose();
            this._connection.Dispose();
            return Task.CompletedTask;
        }

        public async void filterData(Sensor s)
        {
            if (s.PM25 > 250 || s.PM10 > 430 || s.O3 > 748 || s.CO > 34000 || s.NO2 > 400 || s.SO2 > 1600)
            {
                await _repository.Create(s);
                _messageHubContext.Clients.All.SendAsync("send", s);
            }

            if (s.PM25 >= 91 && s.PM25 <= 120)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "PM 2.5",
                    value = s.PM25,
                    type = "Poor"
                };
                await PostRequst(urlController, w);
            }
            else if (s.PM25 >= 121 && s.PM25 <= 250)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "PM 2.5",
                    value = s.PM25,
                    type = "Very Poor"
                };
                await PostRequst(urlController, w);
            }
            else if (s.PM25 > 250)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "PM 2.5",
                    value = s.PM25,
                    type = "Severe"
                };
                await PostRequst(urlController, w);
            }

            if (s.PM10 >= 251 && s.PM10 <= 350)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "PM 10",
                    value = s.PM10,
                    type = "Poor"
                };
                await PostRequst(urlController, w);
            }
            else if (s.PM10 >= 351 && s.PM10 <= 430)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "PM 10",
                    value = s.PM10,
                    type = "Very Poor"
                };
                await PostRequst(urlController, w);
            }
            else if (s.PM10 > 430)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "PM 10",
                    value = s.PM10,
                    type = "Severe"
                };
                await PostRequst(urlController, w);
            }

            if (s.O3 >= 169 && s.O3 <= 208)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "O3",
                    value = s.O3,
                    type = "Poor"
                };
                await PostRequst(urlController, w);
            }
            else if (s.O3 >= 209 && s.O3 <= 748)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "O3",
                    value = s.O3,
                    type = "Very Poor"
                };
                await PostRequst(urlController, w);
            }
            else if (s.O3 > 748)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "O3",
                    value = s.O3,
                    type = "Severe"
                };
                await PostRequst(urlController, w);
            }

            if (s.CO >= 10000 && s.CO <= 17999)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "CO",
                    value = s.CO,
                    type = "Poor"
                };
                await PostRequst(urlController, w);
            }
            else if (s.CO >= 18000 && s.CO <= 34000)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "CO",
                    value = s.CO,
                    type = "Very Poor"
                };
                await PostRequst(urlController, w);
            }
            else if (s.CO > 34000)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "CO",
                    value = s.CO,
                    type = "Severe"
                };
                await PostRequst(urlController, w);
            }

            if (s.NO2 >= 181 && s.NO2 <= 280)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "NO2",
                    value = s.NO2,
                    type = "Poor"
                };
                await PostRequst(urlController, w);
            }
            else if (s.NO2 >= 281 && s.NO2 <= 400)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "NO2",
                    value = s.NO2,
                    type = "Very Poor"
                };
                await PostRequst(urlController, w);
            }
            else if (s.NO2 > 400)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "NO2",
                    value = s.NO2,
                    type = "Severe"
                };
                await PostRequst(urlController, w);
            }

            if (s.SO2 >= 381 && s.SO2 <= 800)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "SO2",
                    value = s.SO2,
                    type = "Poor"
                };
                await PostRequst(urlController, w);
            }
            else if (s.SO2 >= 801 && s.SO2 <= 1600)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "SO2",
                    value = s.SO2,
                    type = "Very Poor"
                };
                await PostRequst(urlController, w);
            }
            else if (s.SO2 > 1600)
            {
                WarningNotification w = new WarningNotification()
                {
                    name = "SO2",
                    value = s.SO2,
                    type = "Severe"
                };
                await PostRequst(urlController, w);
            }
        }

        private async Task PostRequst(string _uri, WarningNotification data)
        {
            HttpClient _httpClient = new HttpClient();
            try
            {
                var jsonData = new StringContent(System.Text.Json.JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                using var httpResponse = await _httpClient.PostAsync(_uri, jsonData);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("OK");
                }

            }
            catch (Exception error)
            {
                Console.WriteLine("[Error]Post request: " + error.Message);
            }
        }
    }
}
