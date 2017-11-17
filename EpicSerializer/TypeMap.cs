using System;
using System.Collections.Generic;

namespace EpicSerializer
{
    /// <summary>
    /// Contains maps of type to that type's conversion function.
    /// </summary>
    internal static class TypeMap
    {
        /// <summary>
        /// Supported EpicRecord Types
        /// </summary>
        internal readonly static IDictionary<Type, Func<object, string>> Var = new Dictionary<Type, Func<object, string>>
        {
            { typeof(string), Converters.FromString },
            { typeof(DateTime), Converters.FromDateTime },
            { typeof(DateTime?), Converters.FromDateTime },
            { typeof(short), Converters.FromShort },
            { typeof(short?), Converters.FromShort },
            { typeof(ushort), Converters.FromUShort },
            { typeof(ushort?), Converters.FromUShort },
            { typeof(int), Converters.FromInt },
            { typeof(int?), Converters.FromInt },
            { typeof(uint), Converters.FromUInt },
            { typeof(uint?), Converters.FromUInt },
            { typeof(long), Converters.FromLong },
            { typeof(long?), Converters.FromLong },
            { typeof(ulong), Converters.FromULong },
            { typeof(ulong?), Converters.FromULong },
            { typeof(float), Converters.FromFloat },
            { typeof(float?), Converters.FromFloat },
            { typeof(double), Converters.FromDouble },
            { typeof(double?), Converters.FromDouble },
            { typeof(decimal), Converters.FromDecimal },
            { typeof(decimal?), Converters.FromDecimal },
            { typeof(bool), Converters.FromBoolean },
            { typeof(bool?), Converters.FromBoolean }
        };

        /// <summary>
        /// Supported EpicRepeat Types
        /// </summary>
        internal readonly static IDictionary<Type, Func<object, IEnumerable<string>>> Iter = new Dictionary<Type, Func<object, IEnumerable<string>>>
        {
            { typeof(string), Converters.FromStringIter },
            { typeof(DateTime), Converters.FromDateTimeIter },
            { typeof(short), Converters.FromShortIter },
            { typeof(ushort), Converters.FromUShortIter },
            { typeof(int), Converters.FromIntIter },
            { typeof(uint), Converters.FromUIntIter },
            { typeof(long), Converters.FromLongIter },
            { typeof(ulong), Converters.FromULongIter },
            { typeof(float), Converters.FromFloatIter },
            { typeof(double), Converters.FromDoubleIter },
            { typeof(decimal), Converters.FromDecimalIter }
        };
    }
}