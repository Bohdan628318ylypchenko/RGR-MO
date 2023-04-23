using Optimization1D;

namespace Tests
{
    [TestClass]
    public class SvenTests
    {
        [TestMethod]
        public void IntervalTest1()
        {
            // Running interval search
            var ab = Sven.Interval(x => 5 * x * x - 2000 * x - 43, 198, 5);

            // Asserting
            Assert.IsTrue(ab.a < 200);
            Assert.IsTrue(200 < ab.b);
        }

        [TestMethod]
        public void IntervalTest2()
        {
            // Running interval search
            var ab = Sven.Interval(x => x * x - 8 * x, 5, 0.1);

            // Asserting
            Assert.IsTrue(ab.a < 4);
            Assert.IsTrue(4 < ab.b);
        }

        [TestMethod]
        public void StartPointAsMinTest()
        {
            // Running interval search
            var ab = Sven.Interval(x => x * x , 0, 0.1);

            // Asserting
            Assert.AreEqual(-0.1, ab.a);
            Assert.AreEqual(0.1, ab.b);
        }
    }
}