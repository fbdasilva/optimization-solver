namespace DifferentialEvolution
{
    internal class Solver
    {
        Random rdm = new Random();

        // fitness function
        public double fitnessFunction(double[] y)
        {
            double f_y = Math.Sin(y[0]) + Math.Cos(y[1]) + (y[1] / 5) + 4;
            return f_y;
        }

        //Generate initial population
        public double[] initialY1(int popSize, double min, double max) {
            double[] xi = new double[popSize];
            for (int i = 0; i < popSize; i++) {
                xi[i] = rdm.NextDouble()*(max-min) + min;
            }
            return xi;
        }
        public double[] initialY2(int popSize, double min, double max)
        {
            double[] yi = new double[popSize];
            for (int i = 0; i < popSize; i++) {
                yi[i] = rdm.NextDouble()*(max-min) + min;
            }
            return yi;
        }

        public void solution()
        {
            // solver parameters
            int popSize = 20;           // population size
            double pCs = 0.5;           // crossover possibility
            double F = 0.5;             // amplification factor
            double iterMax = 50;        // maximum iterations
            int dim = 2;                // dimension of the problem

            double[] minY = {-10,-10};
            double[] maxY = { 10, 10};

            // define initial population
            double[] y1 = initialY1(popSize, minY[0], maxY[0]);
            double[] y2 = initialY2(popSize, minY[1], maxY[1]);

            double[] u = new double[dim];           // test vector
            double[] Xi = new double[dim];          // previous value
            double[] Xi_t = new double[dim];        // target vector
            double iter = 0;        // iteration counter
            int r1, r2, r3;         // random population individual index
            double s;               // crossover

            while (iter < iterMax)
            {
                iter++;
                for (int i = 0; i < popSize; i++) {
                    Xi[0] = y1[i];          // previous value
                    Xi[1] = y2[i];          // previous value

                    // choose 3 individuals (r1 != r2 != r3 !=i)
                    r1 = rdm.Next(0, popSize-1);
                    r2 = rdm.Next(0, popSize-1);
                    r3 = rdm.Next(0, popSize-1);

                    while (true)
                    {
                        if(r1 == i)
                        {
                            r1 = rdm.Next(0, popSize - 1);
                        }
                        if(r2 == i || r2 == r1)
                        {
                            r2 = rdm.Next(0, popSize - 1);
                        }
                        if(r3 == i || r3 == r1 || r3 == r2)
                        {
                            r3 = rdm.Next(0, popSize - 1);
                        }
                        if(i!=r1 && i!=r2 && i!=r3 && r1!=r2 && r1!=r3 && r2 != r3)
                        {
                            break;
                        }
                    }
                    
                    // test vector
                    u[0] = y1[r1] - F * (y1[r2] - y1[r3]);
                    u[1] = y2[r1] - F * (y2[r2] - y2[r3]);

                    for (int j = 0; j < dim; j++) {
                        s = rdm.NextDouble();      // random number between 0 and 1 
                        if (s < pCs && u[j] < maxY[j] && u[j] > minY[j])
                        {
                            Xi_t[j] = u[j];

                        }
                        else
                        {
                            Xi_t[j] = Xi[j];
                        }
                    }

                    if(fitnessFunction(Xi_t) < fitnessFunction(Xi))
                    {
                        // update population
                        y1[i] = Xi_t[0];
                        y2[i] = Xi_t[1];
                    }
                }
            }

            // Final population
            List<double> f_pop = new List<double>();
            Dictionary<double,int> solution_pop = new Dictionary<double,int>();

            for (int n = 0; n < popSize; n++)
            {
                double[] yn = {y1[n], y2[n]};
                f_pop.Add(fitnessFunction(yn));
                Console.WriteLine("Item " + n + " from population: \n" +
                    "f(" + y1[n].ToString() + 
                    ", " + y2[n].ToString() + 
                    " ) = " + f_pop[n] + ".");

            }

            int index = 0;

            double fmin = f_pop.Min();
            if (f_pop.Count > 0)
            {
                index = f_pop.IndexOf(fmin);
            }
            Console.WriteLine(" \n Min values -> y1: " + y1[index].ToString() +
                " - y2: " + y2[index].ToString() +
                " - f(y1,y2): " + f_pop[index].ToString());
        }

    }
}
