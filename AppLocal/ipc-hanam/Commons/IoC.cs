using AFS.HMILibrary.Wpf;
using AFSClient;
using ipc_hanam.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ipc_hanam
{
    public class IoC
    {
        public static IoC Instance { get; private set; } = new IoC();

        public string AppPath { get; private set; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public AppSettings AppSettings { get; set; }

        public StationModel Station { get; set; }

        public string TagPrefix { get; set; } = "0/Connectivity/summary/tags";

        public IServerConnector ServerConnector { get; set; }

        System.Timers.Timer _timerLog;
        private LogSumaryModel logSumary = new LogSumaryModel();//chứa giá trị để log data theo thời gian 5 phút
        private int dbLogInterval = 0;

        private IoC()
        {
            ServerConnector = ServerConnectorProvider.GetConnector(0);

            Station = new StationModel();
            Station.ModuleWatt = 450;
            Station.RatedACCapacitor = 4000;
            Station.RatedDCCapacity = 4887.9;
            Station.TotalModules = 10862;
            Station.TotalInverters = 40;

            for (int i = 1; i <= 7; i++)
            {
                LoggerModel logger = new LoggerModel()
                {
                    DisplayName = $"Logger PVS {i}",
                    Index = i,
                    DeviceId = $"{i}",
                    Model = "Smart Logger Huawei",
                    TagPrefix = $"0/Connectivity/pvs{i}/logger"
                };
                Station.Loggers.Add(logger);

                if (i == 3 || i == 7)
                {
                    for (int j = 1; j <= 5; j++)
                    {
                        InverterModel inverter = new InverterModel()
                        {
                            DisplayName = $"Inverter {j}",
                            Model = "SUN2000-10pKTL-M1x",
                            Status = "",
                            DeviceId = "",
                            Index = j,
                            TagPrefix = $"0/Connectivity/pvs{i}/invt{j}",
                            HistoryPath = $"Historians/pvs{i}_invt{j}",
                            AlarmPath = $"Alarm loggers/pvs{i}_invt{j}",
                        };
                        logger.Inverters.Add(inverter);
                    }
                }
                else
                {
                    for (int j = 1; j <= 6; j++)
                    {
                        InverterModel inverter = new InverterModel()
                        {
                            DisplayName = $"Inverter {j}",
                            Model = "SUN2000-10pKTL-M1x",
                            Status = "",
                            DeviceId = "",
                            Index = j,
                            TagPrefix = $"0/Connectivity/pvs{i}/invt{j}",
                            HistoryPath = $"Historians/pvs{i}_invt{j}",
                            AlarmPath = $"Alarm loggers/pvs{i}_invt{j}",
                        };
                        logger.Inverters.Add(inverter);
                    }
                }

                logger.EnergyPowerMeter = new EnergyPowerMeterModel()
                {
                    Model = "PM5310",
                    DisplayName = "Energy Power Meter",
                    DeviceId = $"PVS{i}/PM5310",
                    TagPrefix = $"0/Connectivity/pvs{i}-pm/Device",

                };

                logger.ElectricalQualityMeter = new ElectricalQualityMeterModel()
                {
                    Model = "KN612",
                    DisplayName = "Electrical Quality Meter",
                    DeviceId = $"PVS{i}/KN612",
                    TagPrefix = $"0/Connectivity/pvs{i}-kn612/Device"
                };

                logger.ZeroExportPowerMeter = new ZeroExportPowerMeterModel()
                {
                    Model = "PM5310",
                    DisplayName = "Zero Export Power Meter",
                    DeviceId = $"PVS{i}/ZE-PM5310",
                    TagPrefix = $"0/Connectivity/pvs{i}/zero-export"
                };

                logger.Start();
            }

            this.LoadTags(true);

            ServerConnector.Start();

            dbLogInterval = int.TryParse(ConfigurationManager.AppSettings["LogInterval"], out int value) ? value : 300000;
            _timerLog = new System.Timers.Timer();
            _timerLog.AutoReset = false;
            _timerLog.Interval = GetNextLogInterval(dbLogInterval);
            _timerLog.Elapsed += _timerLog_Elapsed;
            _timerLog.Start();
        }

        private void _timerLog_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timerLog.Enabled = false;

            try
            {
                //Station.Loggers[0].Inverters[0].pv8_current.Value
                logSumary.DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                logSumary.Radiation = radiation != null && !string.IsNullOrEmpty(radiation.Value) ? radiation.Value : "0";
                logSumary.Temperature = panel_temperature != null && !string.IsNullOrEmpty(panel_temperature.Value) ? panel_temperature.Value : "0";
                logSumary.WindSpeed = wind_speed != null && !string.IsNullOrEmpty(wind_speed.Value) ? wind_speed.Value : "0";
                logSumary.Energy = total_energy_yields != null && !string.IsNullOrEmpty(total_energy_yields.Value) ? total_energy_yields.Value : "0";
                logSumary.ActivePower = total_output_power != null && !string.IsNullOrEmpty(total_output_power.Value) ? total_output_power.Value : "0";

                DataProvider.Instance.ExecuteNonQuery($"insert into summary (DateTime,Radiation,Temperature,WindSpeed,Energy,ActivePower) " +
                                                    $"values ('{logSumary.DateTime}','{logSumary.Radiation}','{logSumary.Temperature}','{logSumary.WindSpeed}','{logSumary.Energy}','{logSumary.ActivePower}')");

                Debug.WriteLine($"Log Data: {DateTime.Now.ToString("HH:mm:ss")}");
            }
            catch
            {

            }
            finally
            {
                _timerLog.Interval = GetNextLogInterval(dbLogInterval);
                _timerLog.Start();
            }
        }

        public virtual long GetNextLogInterval(int logInterval)
        {
            DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            TimeSpan timeDiff = DateTime.Now - startTime;
            long totalMilisecond = (long)timeDiff.TotalMilliseconds;
            long nextInterval = logInterval - (totalMilisecond % logInterval);
            if (nextInterval < 0)
                nextInterval = 0;
            nextInterval += 10;
            return nextInterval;
        }

        public TagNode daily_energy_yields { get; set; }
        public TagNode monthly_energy_yields { get; set; }
        public TagNode yearly_energy_yields { get; set; }
        public TagNode total_energy_yields { get; set; }
        public TagNode co2_reduction { get; set; }
        public TagNode coal_saved { get; set; }
        public TagNode tree_planted { get; set; }
        public TagNode total_input_power { get; set; }
        public TagNode total_output_power { get; set; }
        public TagNode radiation { get; set; }
        public TagNode panel_temperature { get; set; }
        public TagNode wind_speed { get; set; }
        public TagNode total_active_power_pm { get; set; }
        public TagNode total_energy_pm { get; set; }
        public TagNode total_active_power_ze { get; set; }
        public TagNode total_energy_ze { get; set; }
    }
}
