using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI4
{
    public class SinSinFunction : IFunction
    {
        public double Calculate(double x)
        {
            return Math.Sin(x / 10) * Math.Sin(x / 200);
        }
    }
}