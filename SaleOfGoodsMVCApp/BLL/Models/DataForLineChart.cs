using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using DAL.Repositories;

namespace BLL.Models
{
    public class DataForLineChart : IDataForChart<LineData>
    {
        private readonly SaleInfoRepository _saleInfoRepository;
        private readonly ProductsRepository _productsRepository;
        
        public DataForLineChart()
        {
            _saleInfoRepository = new SaleInfoRepository();
            _productsRepository = new ProductsRepository();
        }
        
        public IList<LineData> ListDatas
        {
            get { return GetItems(); }
        }

        private IList<LineData> GetItems()
        {
            var list = new List<LineData>();
            foreach (var item in _productsRepository.Items)
            {
                object[] array = new object[12];
                for (int i = 1; i <= 12; i++)
                    array[i - 1] = _saleInfoRepository.Where(it => it.Product.Name == item.Name && it.Date.Month == i 
                        && it.Date.Year == DateTime.Now.Year).TotalCost();
                list.Add(new LineData
                {
                    List = array, 
                    Name = item.Name
                });
            }
            return list;
        }
    }
}
