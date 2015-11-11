using System;

namespace ATSProject.Model
{
    public struct CallStatistic
    {
        public TimeSpan Duration { get; set; }
        public double Cost { get; set; }

        public override string ToString()
        {
            return string.Format("Duration: {0} Cost: {1}", Duration, Cost);
        }
    }
}
