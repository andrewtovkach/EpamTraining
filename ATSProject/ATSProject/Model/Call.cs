namespace ATSProject.Model
{
    public class Call
    {
        public CallInfo Info;
        public CallStatistic Statistic;

        public override string ToString()
        {
            return string.Format("{0} - {1} {2} ({3}) {4}", Info.Source, Info.Target, Info.Date, Info.Type, Statistic);
        }
    }
}
