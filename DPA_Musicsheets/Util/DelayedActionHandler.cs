using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DPA_Musicsheets.Util
{
    public class DelayedActionHandler
    {
        private readonly double _delay;

        private DispatcherTimer _timer;

        public DelayedActionHandler(double delay)
        {
            _delay = delay;
        }

        public void Run(Action action)
        {
            ResetTimer();
            EventHandler handler = null;
            handler = (sen, ev) =>
            {
                _timer.Tick -= handler;
                StopTimer();
                action();
            };

            _timer.Tick += handler;
            _timer.Interval = TimeSpan.FromSeconds(_delay);
            StartTimer();
        }

        private void ResetTimer()
        {
            StopTimer();
            _timer = new DispatcherTimer();
        }

        private void StopTimer()
        {
            _timer?.Stop();
            AfterStop(); // _isGenerating = false;
        }

        protected virtual void AfterStop()
        { }

        private void StartTimer()
        {
            BeforeStart();// _isGenerating = true;
            _timer.Start();
        }

        protected virtual void BeforeStart()
        { }
    }
}
