using CommunityToolkit.Mvvm.ComponentModel;
using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using static H2Desktop_dotNet.ViewModel.TrafficMonitor;

namespace H2Desktop_dotNet.ViewModel
{
    partial class TrafficMonitor:ObservableObject
    {
        DispatcherTimer dispatcherTimer =new DispatcherTimer() { Interval=new TimeSpan(0,0,1)};

        public class OSInfo
        {
            public string OSName { get; set; }
            public string UpTime { get; set; }
            public string AccountName { get; set; }
        }

        public class CPUInfo
        {
            public string CPUName { get; set; }
            public int CPULoadAvr { get; set; }
        }
        public class GPUInfo
        {
            public string GPUName { get; set; }
            public int GPULoadAvr { get; set; }
        }

        public class RAMInfo
        {
            public string Name { get; set; }
            public int LoadAvr { get; set; }

            public string Max { get; set; }
            public string Current { get; set; }
        }
        Computer mycom = new Computer()
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true,
            IsMemoryEnabled = true,
        };

        public class UpdateVisitor : IVisitor
        {
            public void VisitComputer(IComputer computer)
            {
                computer.Traverse(this);
            }
            public void VisitHardware(IHardware hardware)
            {
                hardware.Update();
                foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
            }
            public void VisitSensor(ISensor sensor) { }
            public void VisitParameter(IParameter parameter) { }
        }

        [ObservableProperty]
        private OSInfo myOSInfo;

        [ObservableProperty]
        private CPUInfo myCPUInfo;


        [ObservableProperty]
        private GPUInfo myGPUInfo;

        [ObservableProperty]
        private RAMInfo myRAMInfo;

        public TrafficMonitor()
        {
            dispatcherTimer.Tick += (s, e) =>
            {
                UpdateData();
            };
            dispatcherTimer.Start();
        }

        private void UpdateData()
        {
            MyOSInfo = new OSInfo()
            {
                OSName = Environment.OSVersion.VersionString,
                UpTime = GetUpTime(),
                AccountName = Environment.UserName,
            };


            mycom.Open();
            mycom.Accept(new UpdateVisitor());

            int loadavr = 0;
            

            foreach (var item in mycom.Hardware)
            {
                if (item.HardwareType == HardwareType.Cpu)
                {

                    foreach (var sensor in item.Sensors)
                    {
                        if (sensor.Name== "CPU Total")
                        {
                            loadavr = (int)Math.Round((double)sensor.Value!); 
                        }
                    }

                    MyCPUInfo = new CPUInfo() { CPUName = item.Name, CPULoadAvr = loadavr };

                }
                if (IsGPU(item.HardwareType))
                {
                    foreach (var sensor in item.Sensors)
                    {
                        if (sensor.Name == "GPU Core" && sensor.SensorType==SensorType.Load)
                        {
                            loadavr = (int)Math.Round((double)sensor.Value!);
                        }
                    }
                    MyGPUInfo = new GPUInfo() { GPUName = item.Name, GPULoadAvr = loadavr };

                }
                if (item.HardwareType==HardwareType.Memory)
                {
                    foreach (var sensor in item.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Load && sensor.Name== "Memory")
                        {
                            loadavr = (int)Math.Round((double)sensor.Value!);

                        }
                    }
                    MyRAMInfo = new RAMInfo() { Name = item.Name, LoadAvr = loadavr };

                }

            }
        }

        private bool IsGPU(HardwareType t)
        {
            switch (t)
            {

                case HardwareType.GpuNvidia:
                    return true;
                case HardwareType.GpuAmd:
                    return true;
                case HardwareType.GpuIntel:
                    return true;

                default:
                    return false;
            }
        }


        private string GetUpTime()
        {
            DateTime dateTime = new DateTime(1970, 1, 1);
            
            return dateTime.AddMilliseconds(Environment.TickCount).ToString("T");
        }
    }
}
