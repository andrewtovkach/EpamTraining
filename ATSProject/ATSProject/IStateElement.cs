using System;

namespace ATSProject
{
    public interface IStateElement<T>
    {
        T State { get; set; }
        event EventHandler<T> StateChanged;
        void ClearEvents();
        bool IsOnline { get; }
        bool IsOffline { get; }
    }
}
