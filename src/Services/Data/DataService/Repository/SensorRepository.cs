using DataService.Data;
using DataService.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Repository
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

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Sensor> filter = Builders<Sensor>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Sensors
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<Sensor> GetSensor(string id)
        {
            return await _context
                            .Sensors
                            .Find(s => s.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Sensor>> GetSensorByDate(string date)
        {
            FilterDefinition<Sensor> filter = Builders<Sensor>.Filter.ElemMatch(p => p.Date.ToString(), date);

            return await _context
                          .Sensors
                          .Find(filter)
                          .ToListAsync();
        }

        public async Task<IEnumerable<Sensor>> GetSensors()
        {
            return await _context
                           .Sensors
                           .Find(s => true)
                           .ToListAsync();
        }

        public async Task<IEnumerable<Sensor>> GetSensorsBetweenDate(string date1, string date2)
        {
            var builder = Builders<Sensor>.Filter;

            var filter = builder.Gt(sensor => sensor.Date, date1) & builder.Lt(sensor => sensor.Date, date2);
            return await _context
                           .Sensors
                           .Find(filter)
                           .ToListAsync();
        }

        public async Task<bool> Update(Sensor sensor)
        {
            var updateResult = await _context
                                        .Sensors
                                        .ReplaceOneAsync(filter: s => s.Id == sensor.Id, replacement: sensor);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
