using DoubleDouble;
using System.Collections.ObjectModel;

namespace DoubleDoubleDifferentiate {
    public static class CenteredIntwayDifferential {
        public static ddouble Differentiate(
            Func<ddouble, ddouble> f, ddouble x, int derivative,
            ddouble h, bool taylor_scale = true) {

            return Differentiate(f, x, [derivative], h, taylor_scale).First().value;
        }

        public static IEnumerable<(int derivative, ddouble value)> Differentiate(
            Func<ddouble, ddouble> f, ddouble x, IEnumerable<int> derivatives,
            ddouble h, bool taylor_scale = true) {

            if (!(h > 0) || !ddouble.IsFinite(h)) {
                throw new ArgumentOutOfRangeException(nameof(h));
            }

            if (derivatives.Any(derivative => derivative < 0 || derivative > CenteredIntwayPoints.MaxDerivative)) {
                throw new ArgumentOutOfRangeException(nameof(derivatives));
            }

            Dictionary<int, ddouble> fs = [];

            foreach (int derivative in derivatives) {
                if (derivative == 0) {
                    ddouble f0 = Table(f, x, h, fs, 0);

                    yield return (0, f0);
                    continue;
                }

                ReadOnlyCollection<ddouble> ws = CenteredIntwayPoints.Table[derivative];

                ddouble s = ddouble.Zero;

                if ((derivative & 1) == 1) {
                    for (int i = 1; i < ws.Count; i++) {
                        ddouble fpi = Table(f, x, h, fs, i), fmi = Table(f, x, h, fs, -i);

                        ddouble w = ws[i];

                        s += w * (fpi - fmi);
                    }
                }
                else {
                    ddouble f0 = Table(f, x, h, fs, 0);

                    s += ws[0] * f0;

                    for (int i = 1; i < ws.Count; i++) {
                        ddouble fpi = Table(f, x, h, fs, i), fmi = Table(f, x, h, fs, -i);

                        ddouble w = ws[i];

                        s += w * (fpi + fmi);
                    }
                }

                s /= ddouble.Pow(h, derivative);

                if (taylor_scale) {
                    s *= ddouble.TaylorSequence[derivative];
                }

                yield return (derivative, s);
            }
        }

        private static ddouble Table(Func<ddouble, ddouble> f, ddouble x, ddouble h, Dictionary<int, ddouble> fs, int i) {
            if (!fs.TryGetValue(i, out ddouble fi)) {
                fi = f(x + i * h);
                fs.Add(i, fi);
            }

            return fi;
        }
    }
}
