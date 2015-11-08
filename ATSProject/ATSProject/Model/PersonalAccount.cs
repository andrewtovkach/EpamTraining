using System;
using System.Collections;
using System.Collections.Generic;

namespace ATSProject.Model
{
    public class PersonalAccount : IEnumerable<Tuple<CallInfo, double>>
    {
        private readonly ICollection<Tuple<CallInfo, double>> _listDepts;

        public string Number { get; set; }
        public double SpentMinutes { get; set; }
        public TariffPlan TariffPlan { get; set; }

        public PersonalAccount(string number, TariffPlan tariffPlan)
        {
            _listDepts = new List<Tuple<CallInfo, double>>();
            SpentMinutes = 0;
            TariffPlan = tariffPlan;
            Number = number;
        }

        public double RestOfFreeMinutes
        {
            get
            {
                double res = TariffPlan.FreeMinutes - SpentMinutes;
                return res < 0 ? 0 : res;
            }
        }

        public double OverrunFreeMinutes
        {
            get
            {
                double res = SpentMinutes - TariffPlan.FreeMinutes;
                return res < 0 ? 0 : res;
            }
        }

        public void Calculate(CallInfo info)
        {
            double debt = Debt;
            SpentMinutes += GetMinutes(info.Duration);
            _listDepts.Add(new Tuple<CallInfo, double>(info, Debt - debt));
        }

        private static int GetMinutes(TimeSpan span)
        {
            return span.Hours * 60 + span.Minutes + span.Seconds / 60;
        }

        public double Debt
        {
            get { return OverrunFreeMinutes * TariffPlan.MinutePrice; }
        }

        public double DeptWithSubscriptionFee
        {
            get { return Debt + TariffPlan.SubscriptionFee; }
        }

        public IEnumerator<Tuple<CallInfo, double>> GetEnumerator()
        {
            return _listDepts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
