using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Repositories;

namespace BLL
{
    public class DataForLineChart
    {
        public List<LineData> ListDatas
        {
            get { return GetItems(); }
        }

        private static List<LineData> GetItems()
        {
            var list = new List<LineData>();
            SaleInfoRepository saleInfoRepository = new SaleInfoRepository();
            foreach (var item in new ProductsRepository().Items)
            {
                object[] array = new object[12];
                for (int i = 1; i <= 12; i++)
                    array[i - 1] = saleInfoRepository.Where(it => it.Product.Name == item.Name && it.Date.Month == i 
                        && it.Date.Year == DateTime.Now.Year).TotalCost();
                list.Add(new LineData { List = array, Name = item.Name });
            }
            return list;
        }
    }
}
