using System;

namespace ATSProject.Model.BillingSystem
{
    public struct CallStatistic
    {
        public TimeSpan Duration;
        public double Cost;

        public override string ToString()
        {
            return string.Format("{0} {1}", Duration, Cost);
        }
    }
}
