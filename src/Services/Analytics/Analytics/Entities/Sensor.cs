using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Entities
{
    public class Sensor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }
        public int PM25 { get; set; }
        public int PM10 { get; set; }
        public int SO2 { get; set; }
        public int NO2 { get; set; }
        public int CO { get; set; }
        public int O3 { get; set; }
        public float Temp { get; set; }
        public float Pres { get; set; }
    }
}
