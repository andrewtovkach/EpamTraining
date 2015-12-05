using System.Collections.Generic;

namespace BLL
{
    public class IndexViewModel<T>
    {
        public IEnumerable<T> Elements { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
