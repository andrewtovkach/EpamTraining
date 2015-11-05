using System;

namespace ATSProject
{
    public class CallInfo
    {
        public PhoneNumber Source { get; set; }
        public PhoneNumber Target { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }

        public CallInfo(PhoneNumber source, PhoneNumber target, DateTime date, TimeSpan duration)
        {
            Source = source;
            Target = target;
            Date = date;
            Duration = duration;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}: {2} ({3})", Source, Target, Date, Duration);
        }
    }
}
