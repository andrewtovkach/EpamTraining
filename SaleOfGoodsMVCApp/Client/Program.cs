using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            List<LineData> list = new DataForLineChart().ListLineDatas;
            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
                for(int i = 0; i < item.List.Length; i++)
                {
                    Console.Write(item.List[i] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
