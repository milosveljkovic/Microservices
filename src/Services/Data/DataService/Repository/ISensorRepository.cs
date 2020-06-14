using DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Repository
{
    public interface ISensorRepository
    {
        Task<IEnumerable<Sensor>> GetSensors();
        Task<Sensor> GetSensor(string id);
        Task Create(Sensor Sensor);
        Task<bool> Update(Sensor Sensor);
        Task<bool> Delete(string id);
        Task<IEnumerable<Sensor>> GetSensorsBetweenDate(string date1, string date2);
    }
}
