using System;
using ATSProject.Enums;

namespace ATSProject.Model
{
    public class CallInfo
    {
        public PhoneNumber Source { get; set; }
        public PhoneNumber Target { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public CallType Type { get; set; }
        public Result Result { get; set; }

        public CallInfo(PhoneNumber source, PhoneNumber target, DateTime date, TimeSpan duration, 
            CallType type, Result result)
        {
            Source = source;
            Target = target;
            Date = date;
            Duration = duration;
            Type = type;
            Result = result;
        }

        public override string ToString()
        {
            return string.Format("{0} calling: {1} - {2}: {3} ({4}) - {5}", Type, Source, Target, Date, Duration, Result);
        }
    }
}
