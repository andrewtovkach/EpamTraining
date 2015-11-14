namespace ATSProject.Model
{
    public class Call
    {
        public CallInfo Info;
        public CallStatistic Statistic;

        public Call(CallInfo info, CallStatistic statistic)
        {
            Info = info;
            Statistic = statistic;
        }

        public override string ToString()
        {
            return string.Format("Info: {0}\nStatistic: {1}", Info, Statistic);
        }
    }
}
