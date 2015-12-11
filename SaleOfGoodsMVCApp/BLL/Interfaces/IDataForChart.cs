using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IDataForChart<T>
    {
        IList<T> ListDatas { get; }
    }
}
