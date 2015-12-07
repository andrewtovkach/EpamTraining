using System.Collections.Generic;
using System.Linq;
using DAL.Repositories;

namespace BLL
{
    public class DataForPieChart
    {
        public List<PieData> ListPieDatas 
        {
            get { return GetItems(); } 
        }

        public List<PieData> GetItems()
        {
            SaleInfoRepository saleInfoRepository = new SaleInfoRepository();
            var list = new ProductsRepository().Items.Select(item => new PieData
            {
                Name = item.Name, 
                Y = saleInfoRepository.Where(it => it.Product.Name == item.Name).TotalCost() / saleInfoRepository.TotalCost() * 100
            }).ToList();
            return list;
        }
    }
}
