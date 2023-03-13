using DoubleDouble;
using System;
using System.Collections.ObjectModel;

namespace DoubleDoubleDifferentiate {
    public static class ForwardIntwayDifferential {
        public static ddouble Differentiate(Func<ddouble, ddouble> f, ddouble x, int derivative, ddouble h) {
            if (derivative < 0 || derivative > ForwardIntwayPoints.MaxDerivative) {
                throw new ArgumentOutOfRangeException(nameof(derivative));
            }

            if (derivative == 0) {
                return f(x);
            }

            ReadOnlyCollection<ddouble> ws = ForwardIntwayPoints.Table[derivative];

            ddouble s = ddouble.Zero;

            for (int i = 0; i < ws.Count; i++) {
                ddouble w = ws[i];

                s += w * f(x + i * h);
            }

            s /= ddouble.Pow(h, derivative);

            return s;
        }
    }
}
