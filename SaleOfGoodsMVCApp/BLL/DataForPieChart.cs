using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Repositories;

namespace BLL
{
    public class DataForPieChart
    {
        public List<PieData> ListDatas 
        {
            get { return GetItems(); } 
        }

        public static List<PieData> GetItems()
        {
            SaleInfoRepository saleInfoRepository = new SaleInfoRepository();
            var list = new ProductsRepository().Items.Select(item => new PieData
            {
                Name = item.Name, 
                Y = saleInfoRepository.Where(it => it.Product.Name == item.Name && it.Date.Year == DateTime.Now.Year)
                .TotalCost() / saleInfoRepository.TotalCost() * 100
            }).ToList();
            return list;
        }
    }
}
