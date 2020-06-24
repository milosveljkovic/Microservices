using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Command.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Command.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Command : ControllerBase
    {

        //private static readonly string DeviceURL = "http://localhost:5002/api/device";
        //private static readonly string ActuatorURL = "http://localhost:5005/api/aktuator";

        //docker
        private static readonly string DeviceURL = "http://172.17.0.1:5002/api/device";
        private static readonly string ActuatorURL = "http://172.17.0.1:5005/api/aktuator";

        // GET: api/<Command>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Command>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Command>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Command>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // POST api/command/setSensorSendPeriod
        [HttpPost("setSensorSendPeriod", Name = "setSensorSendPeriod")]
        public async Task<ActionResult> setSensorSendPeriodAsync([FromBody]Period _period)
        {
            try
            {
                using var httpResponse = await PostRequst(DeviceURL + "/setSensorSendPeriod", _period);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }else
                {
                    return BadRequest();
                }
            }
            catch(Exception error)
            {
                return BadRequest(error);
            }
        }

        // POST api/command/setSensorReadPeriod
        [HttpPost("setSensorReadPeriod", Name = "setSensorReadPeriod")]
        public async Task<ActionResult> setSensorReadPeriodAsync([FromBody] Period _period)
        {
            try
            {
                using var httpResponse = await PostRequst(DeviceURL + "/setSensorReadPeriod", _period);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        // POST api/command/turnOnOff
        [HttpPost("turnOnOff", Name = "turnOnOff")]
        public async Task<ActionResult> turnOnOffAsync([FromBody]int _mode)
        {   //mode 1 turnOn , mode 0 - turnOff
            try
            {
                using var httpResponse = await PostRequst(DeviceURL + "/turnOnOff", _mode);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        // POST api/command/setTreshold
        [HttpPost("setTreshold", Name = "setTreshold")]
        public async Task<ActionResult> setTresholdAsync([FromBody] int _tresholdValue)
        {  
            try
            {
                using var httpResponse = await PostRequst(DeviceURL + "/setTreshold", _tresholdValue);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        // POST api/command/sendNotification
        [HttpPost("sendNotification", Name = "sendNotification")]
        public async Task<ActionResult> sendNotificationAsync([FromBody]Notification _notification)
        {
            try
            {
                using var httpResponse = await PostRequst(ActuatorURL + "/receiveNotification", _notification);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        // POST api/command/turnOnOffMiAirPurifier
        [HttpPost("turnOnOffMiAirPurifier", Name = "turnOnOffMiAirPurifier")]
        public async Task<ActionResult> turnOnOffMiAirPurifier([FromBody] int isOn)
        {   //mode 1 turnOn , mode 0 - turnOff
            try
            {
                using var httpResponse = await PostRequst(DeviceURL + "/turnOnOffMiAirPurifier", isOn);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        // POST api/command/setMiAirPurfierCleaningStrength
        [HttpPost("setMiAirPurfierCleaningStrength", Name = "setMiAirPurfierCleaningStrength")]
        public async Task<ActionResult> setMiAirPurfierCleaningStrength([FromBody] int cleaningStrength)
        {   
            try
            {
                using var httpResponse = await PostRequst(DeviceURL + "/setMiAirPurfierCleaningStrength", cleaningStrength);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        private async Task<HttpResponseMessage> PostRequst(string _uri, Object o)
        {
            HttpClient _httpClient = new HttpClient();
            try
            {
                var jsonData = new StringContent(System.Text.Json.JsonSerializer.Serialize(o), Encoding.UTF8, "application/json");
               return await _httpClient.PostAsync(_uri, jsonData);
            }
            catch (Exception error)
            {
                Console.WriteLine("[Error]Post request: " + error.Message);
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
