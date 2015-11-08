namespace ATSProject.Model
{
    public class TariffPlan
    {
        public string Name { get; set; }
        public double SubscriptionFee { get; set; }
        public double FreeMinutes { get; set; }
        public double MinutePrice { get; set; }

        public TariffPlan(string name, double subscriptionFee, double freeMinutes, double minutePrice)
        {
            Name = name;
            SubscriptionFee = subscriptionFee;
            FreeMinutes = freeMinutes;
            MinutePrice = minutePrice;
        }

        public override string ToString()
        {
            return string.Format("{0} - free min: {1}, min price: {2}", Name, FreeMinutes, MinutePrice);
        }
    }
}
