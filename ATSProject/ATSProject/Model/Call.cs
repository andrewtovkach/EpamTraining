namespace ATSProject.Model
{
    public class Call
    {
        public CallInfo Info;
        public CallStatistic Statistic;

        public override string ToString()
        {
            return string.Format("Statistics: {0} ({1}) {2} {3} BYR", Info.Source, Info.Type, Info.Date, Statistic);
        }
    }
}
