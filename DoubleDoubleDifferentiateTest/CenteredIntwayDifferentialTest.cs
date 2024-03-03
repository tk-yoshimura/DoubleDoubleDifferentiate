using DoubleDouble;
using DoubleDoubleDifferentiate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DoubleDoubleDifferentiateTest {
    [TestClass]
    public class CenteredIntwayDifferentialTest {
        [TestMethod]
        public void DifferentiateExpTest() {
            for (int derivative = 0; derivative <= 16; derivative++) {
                ddouble y = CenteredIntwayDifferential.Differentiate(
                    ddouble.Exp, 0, derivative, 0.125
                );

                Console.WriteLine($"{derivative}\t{y}");
            }
        }

        [TestMethod]
        public void DifferentiatePolyTest() {
            for (int derivative = 0; derivative <= 16; derivative++) {
                ddouble y = CenteredIntwayDifferential.Differentiate(
                    (x) => 1 + x + x * x + x * x * x, 0, derivative, 0.125
                );

                Console.WriteLine($"{derivative}\t{y}");
            }
        }

        [TestMethod]
        public void DifferentiateExpArrayTest() {
            foreach ((int derivative, ddouble y) in CenteredIntwayDifferential.Differentiate(
                ddouble.Exp, 0, [0, 1, 2, 3], 0.125)) {
                Console.WriteLine($"{derivative}\t{y}");
            }
        }

        [TestMethod]
        public void DifferentiatePolyArrayTest() {
            foreach ((int derivative, ddouble y) in CenteredIntwayDifferential.Differentiate(
                (x) => 1 + x + x * x + x * x * x, 0, [0, 1, 2, 3], 0.125)) {
                Console.WriteLine($"{derivative}\t{y}");
            }
        }
    }
}
