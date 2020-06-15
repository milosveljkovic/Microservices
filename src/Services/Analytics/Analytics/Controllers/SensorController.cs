using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Analytics.Entities;
using Analytics.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Analytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ISensorRepository _repository;
        private readonly ILogger<SensorController> _logger;

        public SensorController(ISensorRepository repository, ILogger<SensorController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Sensor>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensors()
        {
            var sensors = await _repository.GetSensors();
            return Ok(sensors);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Sensor), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Sensor>> CreateSensor([FromBody] Sensor sensor)
        {
            await _repository.Create(sensor);
            return CreatedAtRoute("GetSensor", new { id = sensor.Id }, sensor);
        }
    }
}
