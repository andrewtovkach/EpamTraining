using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repositories;

namespace BLL
{
    public static class SalesStatistics
    {
        public static double TotalCost(this IEnumerable<SaleInfo> saleInfos)
        {
            return saleInfos.Sum(item => ConverterCurrency.Convert(item.Currency, item.Cost));
        }

        public static double AverageCost(this IEnumerable<SaleInfo> saleInfos)
        {
            return saleInfos.Average(item => ConverterCurrency.Convert(item.Currency, item.Cost));
        }
    }
}
