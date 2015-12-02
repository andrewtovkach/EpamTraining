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
            foreach (var item in new ProductsRepository().Items)
            {
                Console.WriteLine(item);
            }
        }
    }
}
