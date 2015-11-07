namespace ATSProject.Model
{
    public class TariffPlan
    {
        public string Name { get; set; }
        public uint FreeMinutes { get; set; }
        public uint MinutePrice { get; set; }

        public TariffPlan(string name, uint freeMinutes, uint minutePrice)
        {
            Name = name;
            FreeMinutes = freeMinutes;
            MinutePrice = minutePrice;
        }

        public override string ToString()
        {
            return string.Format("{0} - free min: {1}, min price: {2}", Name, FreeMinutes, MinutePrice);
        }
    }
}
