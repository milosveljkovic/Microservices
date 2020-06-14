using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using System.Globalization;
using Device.RabbitMQ;

namespace Device.Entities
{

    public enum CommunicationType
    {
        Http,
        RabbitMq
    }

    public enum ModeType
    {
        On,
        Off
    }

    public class Sensor : ISensor
    {
        private StreamReader _streamReader;
        private List<SensorData> _sensorDataList;
        private int _sendPeriod;
        private int _readPeriod;
        private Timer _readTimer;
        private Timer _sendTimer;
        private CommunicationType _communicationType;
        private ModeType _modeType;
        private Publisher _publisher;
        private int _treshold;
        private SensorData _previosSensorData;

        public Sensor()
        {
            //PATH = .....\bin\Debug\netcoreapp3.1
            string sensor_file_name = "sensor.txt";
            string path_to_sensor = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sensor_file_name);
            _streamReader = new StreamReader(path_to_sensor);
            _sensorDataList = new List<SensorData>();
            _communicationType = CommunicationType.RabbitMq;
            _modeType = ModeType.Off;
            _publisher = new Publisher();
            _treshold = 10;
            _previosSensorData = new SensorData();

            //default_values_for_tumers
            _readPeriod = 1000;
            _sendPeriod = 3000;
            _readTimer = new Timer(_readPeriod); //ms[1000]
            _sendTimer = new Timer(_sendPeriod); //ms[3000]

            _readTimer.Elapsed += ReadTimer_Elapsed;
            _sendTimer.Elapsed += SendTimer_ElapsedAsync;
        }

        private async void SendTimer_ElapsedAsync(object sender, ElapsedEventArgs e)
        {
            SensorData _sensorData = _sensorDataList[0];
            if (!isTresholdValue(_sensorData)){
                try
                {
                    if (_communicationType == CommunicationType.Http)
                    {
                        //should put url in const! here should be url to Data Micoservice
                        await PostRequst("http://localhost:5000/weatherforecast", _sensorData);
                    }
                    else
                    {
                        //RABBITMQ
                        //IMPORTANT: _sensorData properties should be PUBLIC!!!!!!
                        Console.WriteLine("POST PUBLISH");
                        _publisher.SendMessage(_sensorData);
                        _previosSensorData = _sensorDataList[0];
                        _sensorDataList.RemoveAt(0);
                    }
                }
                catch (Exception error)
                {
                    Console.WriteLine("[Error]: "+error.Message);
                }
            }
            else
            {
                Console.WriteLine("[Warning] Treshold value");
                _previosSensorData = _sensorDataList[0];
                _sensorDataList.RemoveAt(0);
            }
        }

        private bool isTresholdValue(SensorData sd)
        {
            float pm25 = (Math.Abs(_previosSensorData.PM25)*_treshold)/100;
            if (Math.Abs(Math.Abs(_previosSensorData.PM25) - Math.Abs(sd.PM25)) < pm25)
            {
                return true;
            }

            float pm10 = (Math.Abs(_previosSensorData.PM10) * _treshold) / 100;
            if (Math.Abs(Math.Abs(_previosSensorData.PM10) - Math.Abs(sd.PM10)) < pm10)
            {
                return true;
            }

            float so2 = (Math.Abs(_previosSensorData.SO2) * _treshold) / 100;
            if (Math.Abs(Math.Abs(_previosSensorData.SO2) - Math.Abs(sd.SO2)) < so2)
            {
                return true;
            }

            float no2 = (Math.Abs(_previosSensorData.NO2) * _treshold) / 100;
            if (Math.Abs(Math.Abs(_previosSensorData.NO2) - Math.Abs(sd.NO2)) < no2)
            {
                return true;
            }

            float co = (Math.Abs(_previosSensorData.C0) * _treshold) / 100;
            if (Math.Abs(Math.Abs(_previosSensorData.C0) - Math.Abs(sd.C0)) < co)
            {
                return true;
            }

            float o3 = (Math.Abs(_previosSensorData.O3) * _treshold) / 100;
            if (Math.Abs(Math.Abs(_previosSensorData.O3) - Math.Abs(sd.O3)) < o3)
            {
                return true;
            }

            return false;
        }

        private void ReadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
               // year,month,day,hour,PM2.5,PM10,SO2,NO2,CO,O3,TEMP,PRES
               // 2013,3,1,0,3,6,13,7,300,85,-2.3,1020.8
                string line = _streamReader.ReadLine();
                string[] words = line.Split(',');
                if (wordsAreOk(words))
                {

                    SensorData _sensorData = getSensorDataFromWords(words);
                    _sensorDataList.Add(_sensorData);
                    Console.WriteLine("Read every 1 sec, just read--->" + _sensorData);
                }
                else
                {
                    Console.WriteLine("[Error] Some data properti are invalid!");
                }

            }
            catch (Exception error)
            {
                Console.WriteLine("[Error] " + error.Message);
            }
        }

        private SensorData getSensorDataFromWords(string[] words)
        {
            int year = Int32.Parse(words[0]);
            int month = Int32.Parse(words[1]);
            int day = Int32.Parse(words[2]);
            int hour = Int32.Parse(words[3]);

            DateTime date = new DateTime(year, month, day, hour, 0, 0);
            int p25 = Int32.Parse(words[4]);
            int pm10 = Int32.Parse(words[5]);
            int so2 = Int32.Parse(words[6]);
            int no2 = Int32.Parse(words[7]);
            int c0 = Int32.Parse(words[8]);
            int o3 = Int32.Parse(words[9]);
            float temp = float.Parse(words[10]);
            float pres = float.Parse(words[11]);

            return new SensorData(date, p25, pm10, so2, no2, c0, o3, temp, pres);

        }

        private bool wordsAreOk(string[] words)
        {
            int numOfWords = words.Length;
            for (int i = 0; i < numOfWords; i++)
            {
                if (words[i].Equals("NA"))
                {
                    return false;
                }
            }
            return true;
        }

        private async Task PostRequst(string _uri, SensorData _sensorData)
        {
            HttpClient _httpClient = new HttpClient();
            try
            {
                var _sensorDataJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(_sensorData), Encoding.UTF8, "application/json");
                using var httpResponse = await _httpClient.PostAsync(_uri, _sensorDataJson);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    _previosSensorData = _sensorDataList[0];
                    _sensorDataList.RemoveAt(0);
                }

            }
            catch (Exception error)
            {
                Console.WriteLine("[Error]Post request: " + error.Message);
            }
        }

        public void setSendPeriod(int newPeriod)
        {
            //period in ms
            this._sendPeriod = newPeriod;
            this._sendTimer.Interval = newPeriod;
        }

        public void setReadPeriod(int newPeriod)
        {
            //period in ms
            this._readPeriod = newPeriod;
            this._readTimer.Interval = newPeriod;
        }

        public int getSendPeriod()
        {
            return this._sendPeriod;
        }

        public int getReadPeriod()
        {
            return this._readPeriod;
        }

        public void turnOnOff(int mode)
        {
            if (this._modeType == ModeType.Off && mode == 1)
            {
                this._modeType = ModeType.On;
                this._readTimer.Start();
                this._sendTimer.Start();
            }
            else if (this._modeType == ModeType.On && mode == 0)
            {
                this._modeType = ModeType.Off;
                this._readTimer.Stop();
                this._sendTimer.Stop();
            }
            else
            {
                Console.WriteLine("[Error]: turnOnOff");
            }
        }

        public void setTreshold(int newTreshold)
        {
            //period in ms
            this._treshold = newTreshold;
        }
    }
}
