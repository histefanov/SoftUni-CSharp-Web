using Chronometer.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Chronometer.Models
{
    public class Chronometer : IChronometer
    {
        private long startTime;
        private long elapsedTime;
        private bool isStopped;

        public Chronometer()
        {
            this.Laps = new List<string>();
        }

        public string GetTime => GetConvertedTime();

        public List<string> Laps { get; set; }

        public string Lap()
        {
            var lap = this.GetTime;
            this.Laps.Add(lap);

            if (!isStopped)
            {
                this.Start();
            }

            return lap;
        }

        public void Reset()
        {
            this.Stop();
            this.elapsedTime = 0;
            this.Laps.Clear();
        }

        public void Start()
        {
            this.startTime = GetCurrentTime();
            isStopped = false;
        }

        public void Stop()
        {
            if (!isStopped)
            {
                UpdateElapsedTime();
            }

            isStopped = true;
        }

        private long GetCurrentTime() 
            => DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        private string GetConvertedTime()
        {
            if (!isStopped)
            {
                UpdateElapsedTime();
                this.Start();
            }

            TimeSpan t = TimeSpan.FromMilliseconds(this.elapsedTime);
            string result = string.Format("{0:D2}:{1:D2}:{2:D4}",
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds);

            return result;
        }

        private void UpdateElapsedTime()
        {
            var currentTime = GetCurrentTime();
            this.elapsedTime += (currentTime - this.startTime);
        }
    }
}
