﻿using DataService.Data;
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

        public async Task<IEnumerable<Sensor>> GetSensors()
        {
            return await _context
                           .Sensors
                           .Find(s => true)
                           .ToListAsync();
        }

        public async Task<IEnumerable<Sensor>> GetSensorsBetweenDate(string date1, string date2,int pm25,int pm10,int so2,int no2,int o3,int co)
        {
            string[] dt1 = date1.Split('-');
            string[] dt2 = date2.Split('-');
            int[] nums1 = dt1.Select(int.Parse).ToArray();
            int[] nums2 = dt2.Select(int.Parse).ToArray();

            DateTime d1 = new DateTime(nums1[0], nums1[1], nums1[2]);
            DateTime d2 = new DateTime(nums2[0], nums2[1], nums2[2]);

            var builder = Builders<Sensor>.Filter;

            var filter = builder.Gte(sensor => sensor.Date, d1) &
                         builder.Lte(sensor => sensor.Date, d2) &
                         builder.Gte(sensor => sensor.PM25, pm25) &
                         builder.Gte(sensor => sensor.PM10, pm10) &
                         builder.Gte(sensor => sensor.SO2, so2) &
                         builder.Gte(sensor => sensor.NO2, no2) &
                         builder.Gte(sensor => sensor.O3, o3) &
                         builder.Gte(sensor => sensor.CO, co);

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
