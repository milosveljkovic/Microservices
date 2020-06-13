using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Entities
{
    public class Sensor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public float Global_active_power { get; set; }
        public float Global_reactive_power { get; set; }
        public float Voltage { get; set; }
        public float Global_intensity { get; set; }
        public int Sub_metering_1 { get; set; }
        public int Sub_metering_2 { get; set; }
        public int Sub_metering_3 { get; set; }
    }
}
