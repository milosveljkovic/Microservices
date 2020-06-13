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
        private readonly IPublisher _publisher;

        public DeviceController(ISensor mySensor, IPublisher publisher)
        {
            _mySensor = mySensor;
            _publisher = publisher;
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
            Period p = new Period();
            p.periodValue = 12340;
            Console.WriteLine("POST PUBLISH");
            _publisher.SendMessage(p);
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
