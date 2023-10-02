using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc_mvvm
{
    // model
    internal class Calc
    {
        public double Add(double x, double y) { return x + y; }
        public double Sub(double x, double y) { return x - y; }
        public double Mult(double x, double y) { return x * y; }
        public double Div(double x, double y) { return x / y; }
    }
}
