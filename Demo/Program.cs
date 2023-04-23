using MathNet.Numerics.LinearAlgebra;
using Optimization;

namespace Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<Vector<double>, double> f = x => 3 * Math.Pow(x[0] - 14, 2) + x[0] * x[1] + 7 * Math.Pow(x[1], 2);
            Vector<double> x0 = CreateVector.DenseOfArray(new double[2] { 21.8, 21.8 });

            Vector<double> h1 = CreateVector.DenseOfArray(new double[2] { 1, 0 });
            Vector<double> h2 = CreateVector.DenseOfArray(new double[2] { 0, 1 });

            LinkedList<Vector<double>> result = PowellOptimizer.Optimize(f, x0, h1, h2, 0.000001);

            double[] X = result.Select(v => v[0]).ToArray();
            double[] Y = result.Select(v => v[1]).ToArray();

            for (var i = 0; i < result.Count; i++)
            {
                Console.Write("{0:F7} ; {1:F7}\n", X[i], Y[i]);
            }

            var plt = new ScottPlot.Plot(1920, 1080);
            plt.AddScatter(X, Y);
            plt.SaveFig("Result.png");
        }
    }
}