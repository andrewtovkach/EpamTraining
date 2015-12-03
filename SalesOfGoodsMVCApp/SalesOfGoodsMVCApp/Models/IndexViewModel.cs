using System.Collections.Generic;

namespace SalesOfGoodsMVCApp.Models
{
    public class IndexViewModel<T>
    {
        public IEnumerable<T> Elements { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
