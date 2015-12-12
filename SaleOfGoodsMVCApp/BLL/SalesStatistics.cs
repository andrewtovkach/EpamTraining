using System.Collections.Generic;
using System.Linq;
using BLL.Models;
using DAL.Models;

namespace BLL
{
    public static class SalesStatistics
    {
        public static double TotalCost(this IEnumerable<SaleInfo> saleInfos)
        {
            var enumerable = saleInfos as SaleInfo[] ?? saleInfos.ToArray();
            return enumerable.Any() ? enumerable.Sum(item => ConverterCurrency.Convert(item.Currency, item.Cost)) : 0;
        }

        public static double AverageCost(this IEnumerable<SaleInfo> saleInfos)
        {
            var enumerable = saleInfos as SaleInfo[] ?? saleInfos.ToArray();
            return enumerable.Any() ? enumerable.Average(item => ConverterCurrency.Convert(item.Currency, item.Cost)) : 0;
        }
    }
}
