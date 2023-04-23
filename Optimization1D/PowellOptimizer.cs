using MathNet.Numerics.LinearAlgebra;
using Optimization1D;

namespace Optimization
{
    /// <summary>
    /// Implements Powell 2d optimization.
    /// </summary>
    public static class PowellOptimizer
    {
        /// <summary>
        /// Powell optimization
        /// </summary>
        /// <param name="f"> Function to find minimum for. </param>
        /// <param name="x0"> Start point. </param>
        /// <param name="h1"> Start direction 1. </param>
        /// <param name="h2"> Start direction 2. </param>
        /// <param name="epsilon"> Search precision. </param>
        /// <returns> List - optimization path. </returns>
        public static LinkedList<Vector<double>> Optimize(Func<Vector<double>, double> f,
                                                          Vector<double> x0, 
                                                          Vector<double> h1, Vector<double> h2,
                                                          double epsilon)
        {
            // 2D optimization variables
            LinkedList<Vector<double>> result = new LinkedList<Vector<double>>();
            result.AddLast(x0);
            Vector<double>[] x = new Vector<double>[3] { x0, null, null };
            Vector<double>[] h = new Vector<double>[2] { h1, h2 };

            // 1D optimization variables
            Func<double, double> g;
            (double a, double b) ab;
            double k;

            // Optimizing
            while(true)
            {
                // Ordinary steps
                for (var i = 0; i < 2; i++)
                {
                    // Making step
                    g = k => f(x[i] + k * h[i]);
                    ab = Sven.Interval(g, 0, 0.1 * (x[i].L2Norm() / h[i].L2Norm()));
                    k = GoldenOptimizer1D.Optimize(g, ab.a, ab.b, epsilon);
                    x[i + 1] = x[i] + k * h[i];

                    // Saving current step
                    result.AddLast(x[i + 1]);

                    // Check for end
                    if (IsEnd(f, x[i], x[i + 1], epsilon))
                    {
                        // Returning
                        return result;
                    }
                }

                // Direction change
                h[0] = h[1];
                h[1] = x[2] - x[0];
                
                // Last step in current iteration
                g = k => f(x[2] + k * h[1]);
                ab = Sven.Interval(g, 0, 0.1 * (x[2].L2Norm() / h[1].L2Norm()));
                k = GoldenOptimizer1D.Optimize(g, ab.a, ab.b, epsilon);
                x[0] = x[2] + k * h[1];

                // Saving last step
                result.AddLast(x[0]);
            }
        }

        /// <summary>
        /// Optimization end criteria check.
        /// </summary>
        /// <param name="f"> Function - optimization target. </param>
        /// <param name="xc"> Current point. </param>
        /// <param name="xn"> Next point. </param>
        /// <param name="epsilon"> Optimization precision. </param>
        /// <returns> True if optimized, otherwise false. </returns>
        private static bool IsEnd(Func<Vector<double>, double> f,
                                  Vector<double> xc, Vector<double> xn, double epsilon)
        {
            double fxc = f(xc);
            double fxn = f(xn);
            return Math.Abs(fxn - fxc) / Math.Abs(fxc) <= epsilon;
        }
    }
}
