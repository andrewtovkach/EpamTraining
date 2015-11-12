using System;

namespace ATSProject.Model.BillingSystem
{
    public class TariffPlan
    {
        public string Name { get; set; }
        public double SubscriptionFee { get; set; }
        public double FreeMinutes { get; set; }
        public double MinutePrice { get; set; }
        public double SpentMinutes { get; private set; }

        public TariffPlan(string name, double subscriptionFee, double freeMinutes, double minutePrice)
        {
            SpentMinutes = 0;
            Name = name;
            SubscriptionFee = subscriptionFee;
            FreeMinutes = freeMinutes;
            MinutePrice = minutePrice;
        }

        public override string ToString()
        {
            return string.Format("{0} - free min: {1}, min price: {2}", Name, FreeMinutes, MinutePrice);
        }

        public double RestOfFreeMinutes
        {
            get
            {
                double res = FreeMinutes - SpentMinutes;
                return res < 0 ? 0 : res;
            }
        }

        public double OverrunFreeMinutes
        {
            get
            {
                double res = SpentMinutes - FreeMinutes;
                return res < 0 ? 0 : res;
            }
        }

        public double CalculateCost(TimeSpan duration)
        {
            double cost = 0.0;
            double minutes = GetMinutes(duration);
            if (RestOfFreeMinutes < minutes)
                cost = (minutes - RestOfFreeMinutes) * MinutePrice;
            SpentMinutes += minutes;
            return cost;
        }

        private static double GetMinutes(TimeSpan span)
        {
            return span.Hours * 60 + span.Minutes + span.Seconds / 60;
        }
    }
}
