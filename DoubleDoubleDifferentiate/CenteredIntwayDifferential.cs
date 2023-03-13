using DoubleDouble;
using System.Collections.ObjectModel;

namespace DoubleDoubleDifferentiate {
    public static class CenteredIntwayDifferential {
        public static ddouble Differentiate(Func<ddouble, ddouble> f, ddouble x, int derivative, ddouble h) {
            if (derivative < 0 || derivative > CenteredIntwayPoints.MaxDerivative) {
                throw new ArgumentOutOfRangeException(nameof(derivative));
            }

            if (derivative == 0) {
                return f(x);
            }

            ReadOnlyCollection<ddouble> ws = CenteredIntwayPoints.Table[derivative];

            ddouble s = ddouble.Zero;

            if ((derivative & 1) == 1) {
                for (int i = 1; i < ws.Count; i++) {
                    ddouble w = ws[i];

                    s += w * (f(x + i * h) - f(x - i * h));
                }
            }
            else {
                s += ws[0] * f(x);

                for (int i = 1; i < ws.Count; i++) {
                    ddouble w = ws[i];

                    s += w * (f(x + i * h) + f(x - i * h));
                }
            }

            s /= ddouble.Pow(h, derivative);

            return s;
        }
    }
}
