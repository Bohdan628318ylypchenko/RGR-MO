namespace Optimization1D
{
    /// <summary>
    /// Implements 1D Golden cut optimization method.
    /// </summary>
    public class GoldenOptimizer1D
    {
        /// <summary>
        /// Runs Golden cut optimization.
        /// </summary>
        /// <param name="f"> 1D function to optimize. </param>
        /// <param name="a"> Optimization interval start. </param>
        /// <param name="b"> Optimization interval end. </param>
        /// <param name="epsilon"> Optimization precision. </param>
        /// <returns> Function minimum according to Golden Cut. </returns>
        public static double Optimize(Func<double, double> f, 
                                      double a, double b, double epsilon)
        {
            // Interval length
            double l = b - a;
            
            // Length check
            if (l <= epsilon)
            {
                // Length less than epsilon -> end
                return Math.Min(a, b);
            }

            // x1 and x2
            double x1 = a + 0.382 * l;
            double x2 = a + 0.618 * l;

            // Cutting interval
            if (f(x1) > f(x2))
            {
                // Cut from left
                return Optimize(f, x1, b, epsilon);
            }
            else
            {
                // Cut from right
                return Optimize(f, a, x2, epsilon);
            }
        }
    }
}
