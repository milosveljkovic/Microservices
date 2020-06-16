using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.RabbitMQ
{
    public interface IPublisher
    {
        public void SendMessage(object o);
    }
}
