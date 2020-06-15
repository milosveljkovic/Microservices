using Analytics.Data;
using Analytics.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Repository
{
    public class SensorRepository : ISensorRepository
    {
        private readonly IDataContext _context;

        public SensorRepository(IDataContext sensorContext)
        {
            _context = sensorContext ?? throw new ArgumentNullException(nameof(sensorContext));
        }

        public async Task Create(Sensor sensor)
        {
            await _context.Sensors.InsertOneAsync(sensor);
        }

        public async Task<IEnumerable<Sensor>> GetSensors()
        {
            return await _context
                           .Sensors
                           .Find(s => true)
                           .ToListAsync();
        }

        
    }
}
