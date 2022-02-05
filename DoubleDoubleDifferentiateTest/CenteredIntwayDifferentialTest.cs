using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoubleDoubleDifferentiate;
using DoubleDouble;
using System;

namespace DoubleDoubleDifferentiateTest {
    [TestClass]
    public class CenteredIntwayDifferentialTest {
        [TestMethod]
        public void DifferentiateExpTest() {
            for (int derivative = 0; derivative <= 16; derivative++) {
                ddouble y = CenteredIntwayDifferential.Differentiate(ddouble.Exp, 0, derivative, 0.125);

                Console.WriteLine($"{derivative}\t{y}");
            }
        }

        [TestMethod]
        public void DifferentiatePolyTest() {
            for (int derivative = 0; derivative <= 16; derivative++) {
                ddouble y = CenteredIntwayDifferential.Differentiate((x) => 1 + x + x * x + x * x * x, 0, derivative, 0.125);

                Console.WriteLine($"{derivative}\t{y}");
            }
        }
    }
}
