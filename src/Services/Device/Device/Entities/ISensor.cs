using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Entities
{
    public interface ISensor
    {
        void setSendPeriod(int period);
        void setReadPeriod(int period);
        int getSendPeriod();
        int getReadPeriod();
        void turnOnOff(int mode); //mode 1-turnOn , 0-turnOff
        void setTreshold(int newTreshold); //min 1 max 49
        void turnOnOffMiAirPurifier(bool isOn);
        void setMiAirPurfierCleaningStrength(int cleaningStrength);  //max 50, min 10
         int getIsOnSensor();
        int getIsMiAirPurfierOn();
        int getMiAirPurfierCleaningStrength();
        int getTreshold();
    }
}
