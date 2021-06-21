using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TManager.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Process> Processes { get;  } = new ObservableCollection<Process>();
        public MainViewModel()
        {
            var timer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var currentIds = Processes.Select(t => t.Id).ToList();
            foreach (var item in Process.GetProcesses())
            {
                if (currentIds.Remove(item.Id))
                {
                    Processes.Add(item);
                }
            }
            foreach (var item in currentIds)
            {
                _ = Processes.Remove(Processes.First(t => t.Id == item));
            }
        }
    }
}
