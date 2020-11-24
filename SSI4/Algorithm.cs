using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI4
{
    public class Algorithm
    {
        private readonly IFunction _funkcja;
        private readonly (int, int) _przedzial;

        public Algorithm(IFunction funkcja, (int, int) przedzial)
        {
            _funkcja = funkcja;
            _przedzial = przedzial;
        }

        public double Calculate(double rozrzut, double wsp_przyrostu, int l_iteracji)
        {
            var rand = new Random();
            var x = rand.NextDouble() * 100;
            var y = _funkcja.Calculate(x);
            for(int i = 0; i < l_iteracji; i++)
            {
                var xpotRand = new Random();
                var xpot = x + (xpotRand.NextDouble() * rozrzut * 2 - rozrzut);

                if(xpot < _przedzial.Item1)
                {
                    xpot = xpot + rozrzut / 2;
                }

                if(xpot > _przedzial.Item2)
                {
                    xpot = xpot - rozrzut / 2;
                }

                var ypot = _funkcja.Calculate(xpot);

                if(ypot >= y)
                {
                    x = xpot;
                    y = ypot;
                    rozrzut *= wsp_przyrostu;
                }
                else
                {
                    rozrzut /= wsp_przyrostu;
                }
            }

            return y;
        }
    }
}
