using Optimization1D;

namespace Tests
{
    [TestClass]
    public class GoldenCutTests
    {
        [TestMethod]
        public void GoldenCutTest1()
        {
            // Function to minimize
            Func<double, double> g = x => 5 * x * x - 200 * x - 43;

            // Minimum search
            var min = GoldenOptimizer1D.Optimize(g, -100, 50, 0.01);

            // Asserting
            Assert.IsTrue(Math.Abs(min - (20)) < 0.01);
        }

        [TestMethod]
        public void InstantMinTest()
        {
            // Function to minimize
            Func<double, double> g = x => x * x;

            // Minimum search
            var min = GoldenOptimizer1D.Optimize(g, -1, 1, 0.0001);

            // Asserting
            Assert.IsTrue(Math.Abs(min) < 0.0001);
        }
    }
}
