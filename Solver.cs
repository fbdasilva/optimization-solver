namespace DifferentialEvolution
{
    internal class Solver
    {
        int popSize;          // population size
        double pCs;           // crossover possibility
        double F;             // amplification factor
        int dim;              // dimension of the problem
        double iterMax;       // maximum iterations
        double[] minY;        // minimum values of Y
        double[] maxY;        // maximum values of Y
        Random rand = new Random();

        public Solver(int popSize, double pCs, double f, int dim, double iterMax, double[] minY, double[] maxY)
        {
            this.popSize = popSize;
            this.pCs = pCs;
            F = f;
            this.dim = dim;
            this.iterMax = iterMax;
            this.minY = minY;
            this.maxY = maxY;
        }

        public void solution()
        {
            List<double[]> pop = Population.createWithBoundaries(popSize, dim, minY, maxY);
            double[] u = new double[dim];           // test vector
            double[] Xi = new double[dim];          // previous value
            double[] Xi_t = new double[dim];        // target vector
            double iter = 0;                        // iteration counter
            int r1, r2, r3;                         // random population individual index
            double s;                               // crossover chance

            while (iter < iterMax)
            {
                iter++;
                for (int i = 0; i < popSize; i++)
                {
                    // choose 3 individuals (r1 != r2 != r3 !=i)
                    r1 = rand.Next(0, popSize - 1);
                    r2 = rand.Next(0, popSize - 1);
                    r3 = rand.Next(0, popSize - 1);

                    while (true)
                    {
                        if (r1 == i)
                        {
                            r1 = rand.Next(0, popSize - 1);
                        }
                        if (r2 == i || r2 == r1)
                        {
                            r2 = rand.Next(0, popSize - 1);
                        }
                        if (r3 == i || r3 == r1 || r3 == r2)
                        {
                            r3 = rand.Next(0, popSize - 1);
                        }
                        if (i != r1 && i != r2 && i != r3 && r1 != r2 && r1 != r3 && r2 != r3)
                        {
                            break;
                        }
                    }

                    for (int j = 0; j < dim; j++)
                    {
                        double[] var = pop[j];

                        Xi[j] = var[i];          // previous value
                        u[j] = var[r1] - F * (var[r2] - var[r3]);

                        s = rand.NextDouble();      // random number between 0 and 1 
                        if (s < pCs && u[j] < maxY[j] && u[j] > minY[j])
                        {
                            Xi_t[j] = u[j];

                        }
                        else
                        {
                            Xi_t[j] = Xi[j];
                        }
                    }

                    if (FitnessFunction.calculate(Xi_t) < FitnessFunction.calculate(Xi))
                    {
                        int m = 0;
                        foreach (double[] fvar in pop)
                        {
                            fvar[i] = Xi_t[m];
                            m++;
                        }
                    }
                }
            }

            // Final population
            List<double> f_pop = new List<double>();
            Dictionary<double, int> solution_pop = new Dictionary<double, int>();

            double[] y_sol = new double[dim];

            for (int n = 0; n < popSize; n++)
            {
                for (int m = 0; m < dim; m++)
                {
                    y_sol[m] = pop[m][n];
                }
                f_pop.Add(FitnessFunction.calculate(y_sol));

                /*
                Console.WriteLine("Item " + n + " from population: \n" +
                    "f(" + y1[n].ToString() +
                    ", " + y2[n].ToString() +
                    " ) = " + f_pop[n] + ".");*/

            }


            int index = 0;

            double fmin = f_pop.Min();
            if (f_pop.Count > 0)
            {
                index = f_pop.IndexOf(fmin);
            }

            for (int l = 0; l < dim; l++)
            {
                Console.WriteLine("Y[" + l.ToString() + "] = " + pop[l][index]);
            }

            Console.WriteLine(" \n Min values " +
                " - f(y): " + f_pop[index].ToString());

            double[] vs = { -1.5702356075840562, -9.6258317313191 };

        }

    }
}
