using System;
using System.Threading;

namespace SSI4
{
    class Program
    {
        private static readonly (int, int) Przedzial = (0, 100);
        static void Main(string[] args)
        {
            var sinsinFunkcja = new SinSinFunction();
            var algorytm = new Algorithm(sinsinFunkcja, (-100,100), Przedzial);

            Console.WriteLine(algorytm.Calculate(6.0, 2.3, 100));
            Console.ReadLine();
        }

    }
}
