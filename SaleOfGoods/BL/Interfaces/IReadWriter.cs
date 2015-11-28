using System.Collections.Generic;
using BL.Model;

namespace BL.Interfaces
{
    public interface IReadWriter
    {
        void Write(IEnumerable<DataRecord> list);
        IEnumerable<DataRecord> Read();
    }
}
