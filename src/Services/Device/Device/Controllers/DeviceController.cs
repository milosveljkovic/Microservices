using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Device.Entities;
using Device.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        private readonly ISensor _mySensor;

        public DeviceController(ISensor mySensor)
        {
            _mySensor = mySensor;
        }

        // GET: api/<DeviceController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DeviceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DeviceController>
        [HttpPost]
        public void Post()
        {
            //kreiraj objekat

            SensorData sensorData = new SensorData();
            var json = JsonConvert.SerializeObject(sensorData);
            var body = Encoding.UTF8.GetBytes(json);

            var factory = new ConnectionFactory();// { HostName = "rabbitmq",Port=5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

                var message = "TEST SAMOOO";
                //probam da saljem objekat odozgo da vidim kako ce da se ponasa zato je 
                //zakomentarisano ovo ispod var body ...
                //var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "logs",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
            //_publisher.SendMessage("DA LI SI PRIMIO PORUKU");
        }

        // POST api/device/setSensorSendPeriod
        [HttpPost("setSensorSendPeriod", Name = "setSensorSendPeriod")]
        public ActionResult setSensorSendPeriod([FromBody]Period _period)
        {
            if ((_period.periodValue > 0) && (_period.periodValue > _mySensor.getReadPeriod()))
            {
                _mySensor.setSendPeriod(_period.periodValue);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/device/setSensorReadPeriod
        [HttpPost("setSensorReadPeriod", Name = "setSensorReadPeriod")]
        public ActionResult setSensorReadPeriod([FromBody]Period _period)
        {
            if ((_period.periodValue > 0) && (_period.periodValue < _mySensor.getSendPeriod()))
            {
                _mySensor.setReadPeriod(_period.periodValue);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/device/turnOnOff
        [HttpPost("turnOnOff", Name = "turnOnOff")]
        public ActionResult turnOnOff([FromBody]int mode)
        {
            Console.WriteLine("TurnOn Of" + mode);
            if (mode == 1 || mode == 0)
            {
                _mySensor.turnOnOff(mode);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
