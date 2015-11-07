using System;

namespace ATSProject.Interfaces
{
    public interface IStateElement<T>
    {
        T State { get; set; }
        event EventHandler<T> StateChanged;
        void RegistrationEvents();
        void ClearEvents();
    }
}
