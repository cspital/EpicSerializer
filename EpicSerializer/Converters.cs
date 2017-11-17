using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EpicSerializer
{
    /// <summary>
    /// Collection of conversion functions for supported data types.
    /// </summary>
    internal static class Converters
    {
        internal static string FromString(object o)
        {
            var s = (string)o;
            if (String.IsNullOrWhiteSpace(s))
            {
                return null;
            }
            return s;
        }

        internal static string FromDateTime(object o) => o == null ? null : Convert.ToDateTime(o).ToString("MM/dd/yyyy");

        internal static string FromShort(object o) => o == null ? null : Convert.ToInt16(o).ToString(CultureInfo.InvariantCulture);

        internal static string FromUShort(object o) => o == null ? null : Convert.ToUInt16(o).ToString(CultureInfo.InvariantCulture);

        internal static string FromInt(object o) => o == null ? null : Convert.ToInt32(o).ToString(CultureInfo.InvariantCulture);

        internal static string FromUInt(object o) => o == null ? null : Convert.ToUInt32(o).ToString(CultureInfo.InvariantCulture);

        internal static string FromLong(object o) => o == null ? null : Convert.ToInt64(o).ToString(CultureInfo.InvariantCulture);

        internal static string FromULong(object o) => o == null ? null : Convert.ToUInt64(o).ToString(CultureInfo.InvariantCulture);

        internal static string FromFloat(object o) => o == null ? null : Convert.ToSingle(o).ToString(CultureInfo.InvariantCulture);

        internal static string FromDouble(object o) => o == null ? null : Convert.ToDouble(o).ToString(CultureInfo.InvariantCulture);

        internal static string FromDecimal(object o) => o == null ? null : Convert.ToDecimal(o).ToString(CultureInfo.InvariantCulture);

        internal static string FromBoolean(object o) => o == null ? null : Convert.ToInt16((bool)o).ToString(CultureInfo.InvariantCulture);

        internal static IEnumerable<string> FromStringIter(object o) => o == null ? null : (IEnumerable<string>)o;

        internal static IEnumerable<string> FromDateTimeIter(object o) => o == null ? null : ((IEnumerable<DateTime>)o).Select(d => d.ToString("MM/dd/yyyy"));

        internal static IEnumerable<string> FromShortIter(object o) => o == null ? null : ((IEnumerable<short>)o).Select(s => s.ToString(CultureInfo.InvariantCulture));

        internal static IEnumerable<string> FromUShortIter(object o) => o == null ? null : ((IEnumerable<ushort>)o).Select(s => s.ToString(CultureInfo.InvariantCulture));

        internal static IEnumerable<string> FromIntIter(object o) => o == null ? null : ((IEnumerable<int>)o).Select(s => s.ToString(CultureInfo.InvariantCulture));

        internal static IEnumerable<string> FromUIntIter(object o) => o == null ? null : ((IEnumerable<uint>)o).Select(s => s.ToString(CultureInfo.InvariantCulture));

        internal static IEnumerable<string> FromLongIter(object o) => o == null ? null : ((IEnumerable<long>)o).Select(s => s.ToString(CultureInfo.InvariantCulture));

        internal static IEnumerable<string> FromULongIter(object o) => o == null ? null : ((IEnumerable<ulong>)o).Select(s => s.ToString(CultureInfo.InvariantCulture));

        internal static IEnumerable<string> FromFloatIter(object o) => o == null ? null : ((IEnumerable<float>)o).Select(s => s.ToString(CultureInfo.InvariantCulture));

        internal static IEnumerable<string> FromDoubleIter(object o) => o == null ? null : ((IEnumerable<double>)o).Select(s => s.ToString(CultureInfo.InvariantCulture));

        internal static IEnumerable<string> FromDecimalIter(object o) => o == null ? null : ((IEnumerable<decimal>)o).Select(s => s.ToString(CultureInfo.InvariantCulture));
    }
}
