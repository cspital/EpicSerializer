using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EpicSerializer
{
    internal static class Strings
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

        internal static string FromDateTime(object o)
        {
            if (o == null)
            {
                return null;
            }
            return Convert.ToDateTime(o).ToString("MM/dd/yyyy");
        }

        internal static string FromShort(object o)
        {
            if (o == null)
            {
                return null;
            }
            return Convert.ToInt16(o).ToString(CultureInfo.InvariantCulture);
        }

        internal static string FromUShort(object o)
        {
            if (o == null)
            {
                return null;
            }
            return Convert.ToUInt16(o).ToString(CultureInfo.InvariantCulture);
        }

        internal static string FromInt(object o)
        {
            if (o == null)
            {
                return null;
            }
            return Convert.ToInt32(o).ToString(CultureInfo.InvariantCulture);
        }

        internal static string FromUInt(object o)
        {
            if (o == null)
            {
                return null;
            }
            return Convert.ToUInt32(o).ToString(CultureInfo.InvariantCulture);
        }

        internal static string FromLong(object o)
        {
            if (o == null)
            {
                return null;
            }
            return Convert.ToInt64(o).ToString(CultureInfo.InvariantCulture);
        }

        internal static string FromULong(object o)
        {
            if (o == null)
            {
                return null;
            }
            return Convert.ToUInt64(o).ToString(CultureInfo.InvariantCulture);
        }

        internal static string FromFloat(object o)
        {
            if (o == null)
            {
                return null;
            }
            return Convert.ToSingle(o).ToString(CultureInfo.InvariantCulture);
        }

        internal static string FromDouble(object o)
        {
            if (o == null)
            {
                return null;
            }
            return Convert.ToDouble(o).ToString(CultureInfo.InvariantCulture);
        }

        internal static string FromDecimal(object o)
        {
            if (o == null)
            {
                return null;
            }
            return Convert.ToDecimal(o).ToString(CultureInfo.InvariantCulture);
        }

        internal static string FromBoolean(object o)
        {
            if (o == null)
            {
                return null;
            }
            var b = (bool)o;
            return Convert.ToInt16(b).ToString(CultureInfo.InvariantCulture);
        }

        internal static IEnumerable<string> FromStringIter(object o)
        {
            if (o == null)
            {
                return null;
            }
            return (IEnumerable<string>)o;
        }

        internal static IEnumerable<string> FromDateTimeIter(object o)
        {
            if (o == null)
            {
                return null;
            }
            return ((IEnumerable<DateTime>)o)
                .Select(d => d.ToString("MM/dd/yyyy"));
        }

        internal static IEnumerable<string> FromShortIter(object o)
        {
            if (o == null)
            {
                return null;
            }
            return ((IEnumerable<short>)o)
                .Select(s => s.ToString(CultureInfo.InvariantCulture));
        }

        internal static IEnumerable<string> FromUShortIter(object o)
        {
            if (o == null)
            {
                return null;
            }
            return ((IEnumerable<ushort>)o)
                .Select(s => s.ToString(CultureInfo.InvariantCulture));
        }

        internal static IEnumerable<string> FromIntIter(object o)
        {
            if (o == null)
            {
                return null;
            }
            return ((IEnumerable<int>)o)
                .Select(s => s.ToString(CultureInfo.InvariantCulture));
        }

        internal static IEnumerable<string> FromUIntIter(object o)
        {
            if (o == null)
            {
                return null;
            }
            return ((IEnumerable<uint>)o)
                .Select(s => s.ToString(CultureInfo.InvariantCulture));
        }

        internal static IEnumerable<string> FromLongIter(object o)
        {
            if (o == null)
            {
                return null;
            }
            return ((IEnumerable<long>)o)
                .Select(s => s.ToString(CultureInfo.InvariantCulture));
        }

        internal static IEnumerable<string> FromULongIter(object o)
        {
            if (o == null)
            {
                return null;
            }
            return ((IEnumerable<ulong>)o)
                .Select(s => s.ToString(CultureInfo.InvariantCulture));
        }

        internal static IEnumerable<string> FromFloatIter(object o)
        {
            if (o == null)
            {
                return null;
            }
            return ((IEnumerable<float>)o)
                .Select(s => s.ToString(CultureInfo.InvariantCulture));
        }

        internal static IEnumerable<string> FromDoubleIter(object o)
        {
            if (o == null)
            {
                return null;
            }
            return ((IEnumerable<double>)o)
                .Select(s => s.ToString(CultureInfo.InvariantCulture));
        }

        internal static IEnumerable<string> FromDecimalIter(object o)
        {
            if (o == null)
            {
                return null;
            }
            return ((IEnumerable<decimal>)o)
                .Select(s => s.ToString(CultureInfo.InvariantCulture));
        }
    }
}
