using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Repositories;

namespace BLL
{
    public class DataForBarChart
    {
        public List<LineData> ListDatas
        {
            get { return GetItems(); }
        }

        private static List<LineData> GetItems()
        {
            var list = new List<LineData>();
            SaleInfoRepository saleInfoRepository = new SaleInfoRepository();
            ProductsRepository productsRepository = new ProductsRepository();
            for (int i = DateTime.Now.Year - 1; i <= DateTime.Now.Year; i++)
            {
                list.Add(new LineData
                {
                    Name = i + " YEAR", 
                    List = productsRepository.Items.Select(item => saleInfoRepository.Where(it => it.Date.Year == i && it.Product.Name == item.Name)
                        .TotalCost()).Cast<object>().ToArray()
                });
            }
            return list;
        }
    }
}
