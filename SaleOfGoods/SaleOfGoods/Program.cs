using System;
using BL;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser(@"D:\TestFolder\Petrov_21112015.csv");
            Console.WriteLine(parser.ParseFileName().Item1 + " " + parser.ParseFileName().Item2);
        }
    }
}
