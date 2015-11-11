using System;
using ATSProject.Enums;
using ATSProject.Model.ATS;

namespace ATSProject.Model
{
    public struct CallInfo
    {
        public PhoneNumber Source { get; set; }
        public PhoneNumber Target { get; set; }
        public DateTime Date { get; set; }
        public CallType Type { get; set; }
        public Result Result { get; set; }

        public override string ToString()
        {
            return Type == CallType.OutgoingCall ? string.Format("{0} ({1}) - {2}: {3}", Source, Type , Target, Date) 
                : string.Format("{0} ({1}) - {2}: {3}", Target, Type, Source, Date);
        }
    }
}
