using Analytics.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Data
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
                    Date = new DateTime(2008, 12, 12, 0, 0, 0),
                    PM25 = 321,
                    PM10 = 382,
                    SO2 = 13,
                    NO2 = 300,
                    CO = 10,
                    O3 = 100,
                    Temp = 12.5f,
                    Pres = 1051.3f
                },
                new Sensor()
                {
                    Date = new DateTime(2008, 12, 18, 1, 0, 0),
                    PM25 = 412,
                    PM10 = 51,
                    SO2 = 24,
                    NO2 = 350,
                    CO = 10,
                    O3 = 100,
                    Temp = 21.5f,
                    Pres = 1121.3f
                }
            };
        }
    }
}
