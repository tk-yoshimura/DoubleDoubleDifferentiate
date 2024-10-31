using DoubleDouble;
using DoubleDoubleDifferentiate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DoubleDoubleDifferentiateTest {
    [TestClass()]
    public class DifferentiateUtilsTests {
        [TestMethod()]
        public void EstimatePrecisionTest() {
            List<(ddouble h, ddouble value)> values = [];

            for (int exponent = -32; exponent <= -16; exponent++) {
                ddouble h = ddouble.Ldexp(1, exponent);
                ddouble value = ddouble.Pi + ddouble.Square(exponent + 24) * 1e-20;

                values.Add((h, value));

                Console.WriteLine($"{exponent},{value}");
            }

            (ddouble best_h, ddouble best_value, long actual_bits) =
                DifferentiateUtils.EstimatePrecision(values);

            int actual_digits = (int)(actual_bits * 0.30103);

            Console.WriteLine("best");
            Console.WriteLine(double.ILogB((double)best_h));
            Console.WriteLine(best_value);
            Console.WriteLine(best_value.ToString($"e{actual_digits}"));
            Console.WriteLine(actual_digits);
            Console.WriteLine(actual_bits);

            Assert.AreEqual(-24, double.ILogB((double)best_h));
            Assert.AreEqual(20, actual_digits);
        }
    }
}