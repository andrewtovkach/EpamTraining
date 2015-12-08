using System;
using BLL;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new DataForBarChart().ListDatas;
            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
                foreach (var t in item.List)
                {
                    Console.Write(t + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
