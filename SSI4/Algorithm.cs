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
        private readonly (int, int) _zakres_zmiennosc;
        private readonly (int, int) _zakres_x;

        public Algorithm(IFunction funkcja, (int, int) zakres_zmiennosc, (int, int) zakres_x)
        {
            _funkcja = funkcja;
            _zakres_zmiennosc = zakres_zmiennosc;
            _zakres_x = zakres_x;
        }

        public double Calculate(double rozrzut, double wsp_przyrostu, int l_iteracji)
        {
            var rand = new Random();
            var x = rand.NextDouble() * _zakres_x.Item2 * 2 - _zakres_x.Item1;
            var y = _funkcja.Calculate(x);
            for(int i = 0; i < l_iteracji; i++)
            {
                var xpotRand = new Random();
                var xpot = x + (xpotRand.NextDouble() * rozrzut * 2 - rozrzut);

                if(xpot < _zakres_zmiennosc.Item1)
                {
                    xpot = xpot + rozrzut;
                }

                if(xpot > _zakres_zmiennosc.Item2)
                {
                    xpot = xpot - rozrzut;
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
