using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using DAL.Repositories;

namespace BLL
{
    public class DataForBarChart : IDataForChart<LineData>
    {
        readonly SaleInfoRepository _saleInfoRepository;
        readonly ProductsRepository _productsRepository;
       
        public DataForBarChart()
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
            for (int i = DateTime.Now.Year - 1; i <= DateTime.Now.Year; i++)
            {
                list.Add(new LineData
                {
                    Name = i + " YEAR", 
                    List = _productsRepository.Items.Select(item => _saleInfoRepository.Where(it => it.Date.Year == i && it.Product.Name == item.Name)
                        .TotalCost()).Cast<object>().ToArray()
                });
            }
            return list;
        }
    }
}
