using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Aktuator.Entities;
using Colorful;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Aktuator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AktuatorController : ControllerBase
    {
        // GET: api/<AktuatorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AktuatorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AktuatorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPost("receiveNotification", Name = "receiveNotification")]
        public ActionResult receiveNotification([FromBody]Notification _notification)
        {
            if (_notification.type.Equals("Poor"))
            {
                string message = "[{0}] {1} has {2} - AQI [{3}-{4}]   ";
                Formatter[] warningMessage = new Formatter[]{
                new Formatter("Poor Warning", Color.Orange),
                new Formatter(_notification.name, Color.Orange),
                new Formatter(_notification.value.ToString(), Color.Orange),
                new Formatter("201", Color.Orange),
                new Formatter("300", Color.Orange)
                };

                Colorful.Console.WriteLineFormatted(message, Color.Orange, warningMessage);
            }
            else if (_notification.type.Equals("Very Poor"))
            {
                string message = "[{0}] {1} has {2} - AQI [{3}-{4}]   ";
                Formatter[] warningMessage = new Formatter[]{
                new Formatter("Very Poor Warning", Color.OrangeRed),
                new Formatter(_notification.name, Color.OrangeRed),
                new Formatter(_notification.value.ToString(), Color.OrangeRed),
                new Formatter("301", Color.OrangeRed),
                new Formatter("400", Color.OrangeRed)
                };

                Colorful.Console.WriteLineFormatted(message, Color.OrangeRed, warningMessage);
            }
            else
            {
                string message = "[{0}] {1} has {2} - AQI [{3}]   ";
                Formatter[] warningMessage = new Formatter[]{
                new Formatter("Severe Warning", Color.Red),
                new Formatter(_notification.name, Color.Red),
                new Formatter(_notification.value.ToString(), Color.Red),
                new Formatter(">401", Color.Red),
                };

                Colorful.Console.WriteLineFormatted(message, Color.Red, warningMessage);
            }
            return Ok();
        }

    }
}
