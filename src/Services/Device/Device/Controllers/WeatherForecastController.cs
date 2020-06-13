using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Device.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Device.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpPost]
        public SensorData Get(SensorData sd)
        {
            Console.WriteLine("Sensor_data in weather"+sd);
            return sd;
        }
    }
}
