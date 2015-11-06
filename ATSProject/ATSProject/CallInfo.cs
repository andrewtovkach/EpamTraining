using System;

namespace ATSProject
{
    public class CallInfo
    {
        public PhoneNumber Source { get; set; }
        public PhoneNumber Target { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public Result Result { get; set; }

        public CallInfo(PhoneNumber source, PhoneNumber target, DateTime date, TimeSpan duration, Result result)
        {
            Source = source;
            Target = target;
            Date = date;
            Duration = duration;
            Result = result;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}: {2} {3} ({4})", Source, Target, Date, Result, Duration);
        }
    }
}
