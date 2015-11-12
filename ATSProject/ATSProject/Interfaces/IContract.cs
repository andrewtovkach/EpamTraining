using System;
using ATSProject.Model.ATS;
using ATSProject.Model.BillingSystem;

namespace ATSProject.Interfaces
{
    public interface IContract
    {
        string Number { get; set; }
        PhoneNumber PhoneNumber { get; set; }
        bool IsPaid { get; }
        void IncreaseDebt(double debt);
        void PayOnDelivery(double debt);
        double RestOfFreeMinutes { get; }
        double OverrunFreeMinutes { get; }
        double CalculateCost(TimeSpan duration);
        bool ChangeTariffPlan(TariffPlan tariffPlan);
    }
}
