using DataService.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Data
{
    public class DataContextSeed
    {
        public static void SeedData(IMongoCollection<Sensor> sensorCollection)
        {
            bool existSensor = sensorCollection.Find(p => true).Any();
            if (!existSensor)
            {
                sensorCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Sensor> GetPreconfiguredProducts()
        {
            return new List<Sensor>()
            {
                new Sensor()
                {
                    Date = "16/12/2006",
                    Time = "17:22:00",
                    Global_active_power = 4.216f,
                    Global_reactive_power = 0.418f,
                    Voltage = 234.84f,
                    Global_intensity = 18.4f,
                    Sub_metering_1 = 0,
                    Sub_metering_2 = 1,
                    Sub_metering_3 = 17
                },
                new Sensor()
                {
                    Date = "16/12/2006",
                    Time = "17:23:00",
                    Global_active_power = 5.36f,
                    Global_reactive_power = 0.436f,
                    Voltage = 233.84f,
                    Global_intensity = 23f,
                    Sub_metering_1 = 0,
                    Sub_metering_2 = 1,
                    Sub_metering_3 = 16
                }

            };
        }
    }
}
