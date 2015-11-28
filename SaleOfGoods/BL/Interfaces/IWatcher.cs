using System;
using BL.Model;

namespace BL.Interfaces
{
    public interface IWatcher
    {
        void Run(Func<bool> func);
        event EventHandler<FileInformation> CreatedFile;
    }
}
