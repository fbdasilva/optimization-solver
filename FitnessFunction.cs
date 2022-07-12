using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialEvolution
{
    internal class FitnessFunction
    {
        public static double calculate(double[] y)
        {
            double f_y = Math.Sin(y[0]) + Math.Cos(y[1]) + (y[1] / 5) + 4;
            return f_y;
        }
    }
}
