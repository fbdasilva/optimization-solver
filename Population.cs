using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialEvolution
{
    internal class Population
    {
        public static List<double[]> createWithBoundaries(int popSize, int dim, double[] min, double[] max)
        {
            List<double[]> population = new List<double[]>();
            Random rdm = new Random();

            for (int i = 0; i < dim; i++)
            {
                double[] yi = new double[popSize];
                for (int j = 0; j < popSize; j++)
                {
                    yi[j] = rdm.NextDouble() * (max[i] - min[i]) + min[i];
                }
                population.Add(yi);
            }
            return population;
        }
    }
}
