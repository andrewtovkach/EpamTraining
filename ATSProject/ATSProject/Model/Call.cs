namespace ATSProject.Model
{
    public class Call
    {
        public CallInfo Info;
        public CallStatistic Statistic;

        public override string ToString()
        {
            return Info + " " + Statistic;
        }
    }
}
