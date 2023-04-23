namespace Optimization1D
{
    /// <summary>
    /// Sven algorithm implementation.
    /// </summary>
    public class Sven
    {
        /// <summary>
        /// Calculates interval for function.
        /// </summary>
        /// <param name="f"> 1D function to calculate for. </param>
        /// <param name="x0"> Start point. </param>
        /// <param name="delta"> Delta of step </param>
        /// <returns> Tuple - interval. </returns>
        public static (double a, double b) Interval(Func<double, double> f, 
                                                    double x0, double delta)
        {
            // Selecting direction and updating delta
            var dd = SelectDirectionUpdateDelta(f, x0, delta);
            
            // Zero direction check
            if (dd.direction == 0)
            {
                // Zero direction -> x0 - delta, x0 + delta contains min
                return (x0 - delta, x0 + delta);
            }
            else
            {
                // Iterating in selected direction
                return SvenIterate(f, x0, dd.direction * dd.updatedDelta);
            }
        }

        /// <summary>
        /// Selects direction to move at.
        /// Updates 
        /// </summary>
        /// <param name="f"> Function to minimize. </param>
        /// <param name="x0"> Start point. </param>
        /// <param name="d"> Delta of step. </param>
        /// <returns> Selected direction and (possibly) updated delta. </returns>
        private static (int direction, double updatedDelta) SelectDirectionUpdateDelta(Func<double, double> f, 
                                                                                       double x0, double d)
        {
            // Interval
            var points = new double[3] { x0 - d, x0, x0 + d };

            // Bound - middle function values
            var fpoints = points.Select(p => f(p)).ToArray();
            if (fpoints[0] >= fpoints[1] && fpoints[1] >= fpoints[2])
            {
                // Move right, no delta correction
                return (1, d);
            }
            else if (fpoints[0] <= fpoints[1] && fpoints[1] <= fpoints[2])
            {
                // Move left, no delta correction
                return (-1, d);
            }
            else if (fpoints[0] >= fpoints[1] && fpoints[1] <= fpoints[2])
            {
                // Don't move
                return (0, d);
            }
            else
            {
                // Function is multi-modal on interval, try smaller interval.
                return SelectDirectionUpdateDelta(f, x0, d / 2);
            }
        }

        /// <summary>
        /// Iterates according to Sven algorithm.
        /// </summary>
        /// <param name="f"> Function to search interval for. </param>
        /// <param name="cborder"> Current bound. </param>
        /// <param name="step"> Current step. </param>
        /// <returns> Tuple - final interval. </returns>
        private static (double, double) SvenIterate(Func<double, double> f, 
                                                    double cborder, double step)
        {
            // Calculating new border
            double nborder = cborder + step;

            // Is value in new border bigger than current border value?
            if (f(cborder) <= f(nborder))
            {
                // Yes -> go back and return
                cborder -= (step / 2);
                return (Math.Min(cborder, nborder), Math.Max(cborder, nborder));
            }
            else
            {
                // No -> go further
                return SvenIterate(f, nborder, step * 2);
            }
        }
    }
}
