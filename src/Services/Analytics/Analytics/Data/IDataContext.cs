using Analytics.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Data
{
    public interface IDataContext
    {
        IMongoCollection<Sensor> Sensors { get; }
    }
}
