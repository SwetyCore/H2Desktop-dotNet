using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace H2Desktop_dotNet.ViewModel
{
    partial class Clock : ObservableObject
    {
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };

        public Clock()
        {
            timer.Tick += (s, e) =>
            {
                TimeNow = DateTime.Now.ToString("T");
            };
            timer.Start();
        }

        [ObservableProperty]
        private string _timeNow;
    }
}
