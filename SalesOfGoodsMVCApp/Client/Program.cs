using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            SaleInfoRepository saleInfoRepository = new SaleInfoRepository();
            foreach (var item in saleInfoRepository)
            {
                Console.WriteLine(item);
            }
        }
    }
}
