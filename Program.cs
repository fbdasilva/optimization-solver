using DifferentialEvolution;

int populationSize = 30;
double crossoverPossibility = 0.6;
double amplificationFactor = 0.5;
int problemDimension = 2;
double maximumIterations = 50;
double[] minY = { -10, -10};
double[] maxY = { 10, 10};

Solver sv = new Solver(populationSize, crossoverPossibility, amplificationFactor,
    problemDimension, maximumIterations, minY, maxY);
sv.solution();




