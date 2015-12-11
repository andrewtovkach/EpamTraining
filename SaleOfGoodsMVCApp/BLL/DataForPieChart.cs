using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using DAL.Repositories;

namespace BLL
{
    public class DataForPieChart : IDataForChart<PieData>
    {
        readonly SaleInfoRepository _saleInfoRepository;
        readonly ProductsRepository _productsRepository;

        public DataForPieChart()
        {
            _saleInfoRepository = new SaleInfoRepository();
            _productsRepository = new ProductsRepository();
        }

        public IList<PieData> ListDatas 
        {
            get { return GetItems(); } 
        }

        public IList<PieData> GetItems()
        {
            var list = _productsRepository.Items.Select(item => new PieData
            {
                Name = item.Name, 
                Y = _saleInfoRepository.Where(it => it.Product.Name == item.Name && it.Date.Year == DateTime.Now.Year)
                .TotalCost() / _saleInfoRepository.TotalCost() * 100
            }).ToList();
            return list;
        }
    }
}
