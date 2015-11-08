using System;

namespace ATSProject.Model
{
    public class Contract
    {
        public string Number { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public TariffPlan TariffPlan { get; set; }
        public PersonalAccount PersonalAccount { get; set; }
        public DateTime Date { get; set; }

        public Contract(string number, string phoneNumber, TariffPlan tariffPlan, string accountNumber,
            string date)
        {
            Number = number;
            PhoneNumber = new PhoneNumber { Value = phoneNumber };
            TariffPlan = tariffPlan;
            PersonalAccount = new PersonalAccount(accountNumber, TariffPlan);
            Date = DateTime.Parse(date);
        }

        public override string ToString()
        {
            return string.Format("Contract №{0} {1} - {2} ({3})", Number, PhoneNumber, TariffPlan, Date);
        }
    }
}
