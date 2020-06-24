using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataService.Entities;
using DataService.Entities.Objects;
using DataService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataService.Controllers
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

        [HttpGet("{id}", Name = "GetSensor")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Sensor), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Sensor>> GetSensor(string id)
        {
            var sensor = await _repository.GetSensor(id);
            if (sensor == null)
            {
                _logger.LogError($"Sensor with id: { id}, hasn’t been found in database.");
                return NotFound();
            }
            return Ok(sensor);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Sensor), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Sensor>> CreateSensor([FromBody] Sensor sensor)
        {
            await _repository.Create(sensor);
            return CreatedAtRoute("GetSensor", new { id = sensor.Id }, sensor);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Sensor), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateSensor([FromBody] Sensor value)
        {
            return Ok(await _repository.Update(value));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteSensorById(string id)
        {
            return Ok(await _repository.Delete(id));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(Sensor), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensorsBetweenDates([FromBody]Filter filter)
        {

            var sensors = await _repository.GetSensorsBetweenDate(filter.DateFrom, filter.DateTo, filter.PM25, filter.PM10, filter.SO2, filter.NO2, filter.O3, filter.CO);
            return Ok(sensors);
        }
    }
}
