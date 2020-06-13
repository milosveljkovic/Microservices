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

        public Sensor()
        {
            //PATH = .....\bin\Debug\netcoreapp3.1
            string sensor_file_name = "sensor.txt";
            string path_to_sensor = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sensor_file_name);
            _streamReader = new StreamReader(path_to_sensor);
            _sensorDataList = new List<SensorData>();
            _communicationType = CommunicationType.Http;
            _modeType = ModeType.Off;

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
            try
            {
                if (_communicationType == CommunicationType.Http)
                {
                    Console.WriteLine("In send");
                    SensorData _sensorData = _sensorDataList[0];
                    //should put url in const! here should be url to Data Micoservice
                    await PostRequst("http://localhost:5000/weatherforecast", _sensorData);
                }
                else
                {
                    Console.WriteLine("Should implemet RabbitMQ");
                }
            }
            catch
            {
                Console.WriteLine("Error handler in SendTimer.Elapsed");
            }
        }

        private void ReadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                string line = _streamReader.ReadLine();
                string[] words = line.Split(';');
                //correct data example : 16/12/2006;17:26:00;5.374;0.498;233.290;23.000;0.000;2.000;17.000
                //bad data example : 16/12/2006;17:26:00;?;?;?;23.000;0.000;2.000;17.00 bad data, it holds ?
                if (wordsAreOk(words))
                {
                    string _date = words[0];
                    DateTime dt = DateTime.ParseExact(_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //just mock datatype
                    string time = words[1];
                    float val2 = float.Parse(words[2]);
                    float val3 = float.Parse(words[3]);
                    float val4 = float.Parse(words[4]);
                    float val5 = float.Parse(words[5]);
                    float val6 = float.Parse(words[6]);
                    float val7 = float.Parse(words[7]);
                    float val8 = float.Parse(words[8]);
                    SensorData _sensorData = new SensorData(dt, time, val2, val3, val4, val5, val6, val7, val8);
                    _sensorDataList.Add(_sensorData);
                    Console.WriteLine("Read every 1 sec, just read--->" + _sensorData);
                }
                else
                {
                    Console.WriteLine("[Error] Something went bad with read data");
                }

            }
            catch (Exception error)
            {
                Console.WriteLine("[Error] " + error.Message);
            }
        }

        private bool wordsAreOk(string[] words)
        {
            int numOfWords = words.Length;
            for (int i = 0; i < numOfWords; i++)
            {
                if (words[i].Equals("?"))
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
    }
}
