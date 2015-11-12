using System;
using System.Collections;
using System.Collections.Generic;

namespace ATSProject.Model.BillingSystem
{
    public class PersonalAccount : IEnumerable<Tuple<DateTime, double>>
    {
        public string Number { get; set; }
        public double Debt { get; private set; }
        public DateTime MaturityDate { get; set; }

        private readonly ICollection<Tuple<DateTime, double>> _listPayments;

        public PersonalAccount(string number, DateTime maturityDate)
        {
            _listPayments = new List<Tuple<DateTime, double>>();
            Debt = 0;
            Number = number;
            MaturityDate = maturityDate;
        }

        public bool IsPaid
        {
            get { return Debt <= 0; }
        }

        public void IncreaseDebt(double debt)
        {
            Debt += debt;
        }

        public void PayOnDelivery(double debt)
        {
            Debt -= debt;
            _listPayments.Add(new Tuple<DateTime, double>(DateTime.Now, debt));
            if (Debt <= 0)
                MaturityDate += new TimeSpan(31, 0, 0, 0);
        }

        public override string ToString()
        {
            return string.Format("№{0} {1} {2}", Number, Debt, MaturityDate.ToShortDateString());
        }

        public IEnumerator<Tuple<DateTime, double>> GetEnumerator()
        {
            return _listPayments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
