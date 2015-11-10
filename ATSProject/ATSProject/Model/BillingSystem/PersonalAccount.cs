using System;
using System.Collections;
using System.Collections.Generic;
using ATSProject.Model.ATS;

namespace ATSProject.Model.BillingSystem
{
    public class PersonalAccount : IEnumerable<Tuple<CallInfo, CallStatistic>>
    {
        private readonly ICollection<Tuple<CallInfo, CallStatistic>> _listDepts;

        public string Number { get; set; }
        public double SpentMinutes { get; set; }
        public TariffPlan TariffPlan { get; set; }

        public PersonalAccount(string number, TariffPlan tariffPlan)
        {
            _listDepts = new List<Tuple<CallInfo, CallStatistic>>();
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

        public Tuple<CallInfo, CallStatistic> Calculate(Tuple<CallInfo, CallStatistic> info)
        {
            double debt = Debt;
            SpentMinutes += GetMinutes(info.Item2.Duration);
            CallStatistic callStatistic = new CallStatistic
            {
                Cost = Debt - debt,
                Duration = info.Item2.Duration
            };
            info = new Tuple<CallInfo, CallStatistic>(info.Item1, callStatistic);
            _listDepts.Add(info);
            return info;
        }

        private static double GetMinutes(TimeSpan span)
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

        public IEnumerator<Tuple<CallInfo, CallStatistic>> GetEnumerator()
        {
            return _listDepts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
