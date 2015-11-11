using System;
using ATSProject.Model.ATS;

namespace ATSProject.Model.BillingSystem
{
    public class Contract
    {
        public string Number { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public TariffPlan TariffPlan { get; set; }
        public PersonalAccount PersonalAccount { get; set; }
        public DateTime Date { get; set; }

        public Contract(string number, string phoneNumber, TariffPlan tariffPlan, string personalAccount, string date)
        {
            Number = number;
            PhoneNumber = new PhoneNumber { Value = phoneNumber };
            TariffPlan = tariffPlan;
            Date = DateTime.Parse(date);
            PersonalAccount = new PersonalAccount(personalAccount, Date);
        }

        public bool ChangeTariffPlan(TariffPlan tariffPlan)
        {
            DateTime nowDateTime = DateTime.Now, resultDareTime = Date + new TimeSpan(31, 0, 0, 0);
            if (resultDareTime >= nowDateTime)
                return false;
            TariffPlan = tariffPlan;
            Date = nowDateTime;
            return true;
        }

        public override string ToString()
        {
            return string.Format("Contract №{0} {1} - {2} ({3})", Number, PhoneNumber, TariffPlan, Date);
        }
    }
}
