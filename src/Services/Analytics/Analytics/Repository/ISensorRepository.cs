using Analytics.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Repository
{
    public interface ISensorRepository
    {
        Task<IEnumerable<Sensor>> GetSensors();
        Task Create(Sensor Sensor);
    }
}
