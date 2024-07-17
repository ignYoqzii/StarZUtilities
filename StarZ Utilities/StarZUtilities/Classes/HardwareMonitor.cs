using LibreHardwareMonitor.Hardware;
using System;
using System.Threading;
using static StarZUtilities.Windows.MainWindow;

namespace StarZUtilities.Classes
{
    class HardwareMonitor
    {
        private readonly Computer computer;

        public HardwareMonitor()
        {
            computer = new Computer
            {
                IsCpuEnabled = true,
                IsMemoryEnabled = true,
                IsGpuEnabled = true
            };
            computer.Open();
        }

        public void StartMonitoring()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                while (true)
                {
                    UpdateInformation();
                    UpdateNames();
                    Thread.Sleep(1000); // Update every second
                }
            });
        }

        private void UpdateInformation()
        {
            foreach (var hardwareItem in computer.Hardware)
            {
                hardwareItem.Update();

                switch (hardwareItem.HardwareType)
                {
                    case HardwareType.Cpu:
                        UpdateCpuInformation(hardwareItem);
                        break;
                    case HardwareType.GpuNvidia:
                    case HardwareType.GpuAmd:
                        UpdateGpuInformation(hardwareItem);
                        break;
                    case HardwareType.Memory:
                        UpdateMemoryInformation(hardwareItem);
                        break;
                    default:
                        break;
                }
            }
        }

        private void UpdateCpuInformation(IHardware hardwareItem)
        {
            foreach (var sensor in hardwareItem.Sensors)
            {
                switch (sensor.SensorType)
                {
                    case SensorType.Temperature:
                        double temp = Math.Round(sensor.Value.GetValueOrDefault(), 0);
                        cpuTempTextBlock!.Dispatcher.Invoke(() =>
                        {
                            cpuTempTextBlock!.Text = $"Temperature: {temp}°C";
                        });
                        break;
                    case SensorType.Load when sensor.Name == "CPU Total":
                        double load = Math.Round(sensor.Value.GetValueOrDefault(), 0);
                        cpuLoadTextBlock!.Dispatcher.Invoke(() =>
                        {
                            cpuLoadTextBlock!.Text = $"Load: {load}%";
                        });
                        break;
                }
            }

            foreach (var subHardware in hardwareItem.SubHardware)
            {
                foreach (var fan in subHardware.Sensors)
                {
                    if (fan.SensorType == SensorType.Fan && IsCpuFanSensor(fan))
                    {
                        double fanSpeed = Math.Round(fan.Value.GetValueOrDefault(), 0);
                        cpufanTextBlock!.Dispatcher.Invoke(() =>
                        {
                            cpufanTextBlock!.Text = $"CPU Fan: {fanSpeed} RPM";
                        });
                    }
                }
            }
        }

        private bool IsCpuFanSensor(ISensor fan)
        {
            string lowerName = fan.Name.ToLower();
            return lowerName.Contains("cpu") || lowerName.Contains("fan") ||
                   lowerName.Contains("sys") || lowerName.Contains("cool") ||
                   lowerName.Contains("cooling") || lowerName.Contains("processor");
        }

        private void UpdateGpuInformation(IHardware hardwareItem)
        {
            foreach (var sensor in hardwareItem.Sensors)
            {
                switch (sensor.SensorType)
                {
                    case SensorType.Temperature:
                        double temp = Math.Round(sensor.Value.GetValueOrDefault(), 0);
                        gpuTempTextBlock!.Dispatcher.Invoke(() =>
                        {
                            gpuTempTextBlock!.Text = $"Temperature: {temp}°C";
                        });
                        break;
                    case SensorType.Load:
                        double load = Math.Round(sensor.Value.GetValueOrDefault(), 0);
                        gpuLoadTextBlock!.Dispatcher.Invoke(() =>
                        {
                            gpuLoadTextBlock!.Text = $"GPU Load: {load}%";
                        });
                        break;
                    case SensorType.Fan:
                        double fanSpeed = Math.Round(sensor.Value.GetValueOrDefault(), 0);
                        gpufanTextBlock!.Dispatcher.Invoke(() =>
                        {
                            gpufanTextBlock!.Text = $"GPU Fan: {fanSpeed} RPM";
                        });
                        break;
                }
            }
        }

        private void UpdateMemoryInformation(IHardware hardwareItem)
        {
            foreach (var sensor in hardwareItem.Sensors)
            {
                if (sensor.SensorType == SensorType.Load)
                {
                    double memoryUsage = Math.Round(sensor.Value.GetValueOrDefault(), 0);
                    memoryTextBlock!.Dispatcher.Invoke(() =>
                    {
                        memoryTextBlock!.Text = $"Memory: {hardwareItem.Name} ({memoryUsage}%)";
                    });
                }
            }
        }

        private void UpdateNames()
        {
            foreach (var hardwareItem in computer.Hardware)
            {
                switch (hardwareItem.HardwareType)
                {
                    case HardwareType.Cpu:
                        cpuNameTextBlock!.Dispatcher.Invoke(() =>
                        {
                            cpuNameTextBlock!.Text = $"{hardwareItem.Name}";
                        });
                        break;
                    case HardwareType.GpuNvidia:
                    case HardwareType.GpuAmd:
                        gpuNameTextBlock!.Dispatcher.Invoke(() =>
                        {
                            gpuNameTextBlock!.Text = $"{hardwareItem.Name}";
                        });
                        break;
                    case HardwareType.Motherboard:
                        motherboardTextBlock!.Dispatcher.Invoke(() =>
                        {
                            motherboardTextBlock!.Text = $"Motherboard: {hardwareItem.Name}";
                        });
                        break;
                    default:
                        break;
                }
            }
        }

        ~HardwareMonitor()
        {
            computer.Close();
        }
    }
}
