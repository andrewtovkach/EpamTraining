using System;

namespace ATSProject.Model
{
    public class Contract
    {
        public string Number { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public TariffPlan TariffPlan { get; set; }
        public DateTime CreatedDate { get; set; }

        public Contract(string number, string phoneNumber, TariffPlan tariffPlan, string createdDate)
        {
            Number = number;
            PhoneNumber = new PhoneNumber { Value = phoneNumber };
            TariffPlan = tariffPlan;
            CreatedDate = DateTime.Parse(createdDate);
        }

        public override string ToString()
        {
            return string.Format("Contract №{0} {1} - {2} ({3})", Number, PhoneNumber, TariffPlan, CreatedDate);
        }
    }
}
