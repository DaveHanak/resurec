using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace resurec.Models
{
    /// <summary>
    /// A global timer that can be used to trigger events at a regular 1 second interval.
    /// It is essentially a wrapper around a DispatcherTimer.
    /// </summary>
    public class GlobalTimer
    {
        private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(1) };
        public GlobalTimer() { }
        public bool IsStarted => _timer.IsEnabled;

        public event Action? StateChanged;
        public void Start()
        { 
            if (!_timer.IsEnabled)
            {
                _timer.Start();
                OnStateChanged();
            }
        }

        public void Stop()
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                OnStateChanged();
            }
        }

        public void AddCallback(EventHandler callback)
        {
            _timer.Tick += callback;
        }

        public void RemoveCallback(EventHandler callback)
        {
            _timer.Tick -= callback;
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke();
        }
    }
}
