using DoubleDouble;

namespace DoubleDoubleDifferentiate {
    public static class DifferentiateUtils {
        public static (ddouble h, ddouble value, int actual_bits) EstimatePrecision(
            IEnumerable<(ddouble h, ddouble value)> values) {

            if (values.Count() <= 1 || values.Select(v => v.h).Distinct().Count() != values.Count()) {
                throw new ArgumentException("array require multiple and unique 'h'.", nameof(values));
            }

            static int match_bits2(ddouble v1, ddouble v2) {
                if (!ddouble.IsFinite(v1) || !ddouble.IsFinite(v2)) {
                    return 0;
                }

                ddouble dv = v1 - v2;

                int bits = int.Clamp(int.Max(double.ILogB((double)v1), double.ILogB((double)v2)) - double.ILogB((double)dv), 0, 105);

                return bits;
            }

            static int match_bits3(ddouble v1, ddouble v2, ddouble v3) {
                return int.Min(match_bits2(v1, v2), match_bits2(v2, v3));
            }

            List<(ddouble h, ddouble value)> values_sorted = [.. values.OrderByDescending(v => v.h)];

            if (values.Count() == 2) {
                return (
                    values_sorted[1].h,
                    values_sorted[1].value,
                    match_bits2(values_sorted[0].value, values_sorted[1].value)
                );
            }

            ddouble best_h = ddouble.NaN, best_value = ddouble.NaN;
            int max_match_bits = 0;

            for (int i = 1; i < values_sorted.Count - 1; i++) {
                int match_bits = match_bits3(values_sorted[i - 1].value, values_sorted[i].value, values_sorted[i + 1].value);

                if (match_bits > max_match_bits) {
                    (best_h, best_value) = values_sorted[i];
                    max_match_bits = match_bits;
                }
            }

            return (best_h, best_value, max_match_bits);
        }
    }
}
