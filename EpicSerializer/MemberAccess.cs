using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace EpicSerializer
{
    internal class MemberAccess
    {
        internal Func<object, string> Value { get; private set; }
        internal int Field { get; private set; }
        internal bool OmitIfEmpty { get; private set; }
        internal string Name { get; private set; }
        internal MemberTypes MemberType { get; }

        internal MemberAccess(FieldInfo fi)
        {
            Initialize(fi, fi.FieldType);

            MemberType = MemberTypes.Field;
        }

        internal MemberAccess(PropertyInfo pi)
        {
            Initialize(pi, pi.PropertyType);

            MemberType = MemberTypes.Property;
        }

        private void Initialize(MemberInfo mi, Type memberType)
        {
            var recordAttribute = mi.GetCustomAttribute<EpicRecordAttribute>();
            var repeatAttribute = mi.GetCustomAttribute<EpicRepeatAttribute>();

            // EpicRecordAttribute and EpicRepeatAttribute are mutually exclusive, ~XOR their presence
            if (!(recordAttribute != null ^ repeatAttribute != null))
            {
                throw new EpicSerializerException(String.Format("EpicRecord and EpicRepeat are mutually exclusive, {0}.{1} has applied them both.", mi.DeclaringType.Name, mi.Name));
            }
            Name = mi.Name;

            // member is tagged as EpicRecordAttribute
            if (recordAttribute != null)
            {
                if (!EpicRecordAttribute.ValidTypes.Contains(memberType))
                {
                    throw new EpicSerializerException(String.Format("{0}.{1} is marked as EpicRecord but is not in EpicRecordAttribute.ValidTypes.", mi.DeclaringType.Name, mi.Name));
                }

                Field = recordAttribute.Field;
                OmitIfEmpty = recordAttribute.OmitIfEmpty;
                Value = GetValueFunc(memberType);
            }
            else
            {
                var innerEnumerableType = GetEnumerableType(memberType);
                if (innerEnumerableType == null)
                {
                    throw new EpicSerializerException(String.Format("{0}.{1} is marked EpicRepeat, but is not an IEnumerable<T>.", mi.DeclaringType.Name, mi.Name));
                }

                if (!EpicRepeatAttribute.ValidTypes.Contains(innerEnumerableType))
                {
                    // complex type
                    if (!innerEnumerableType.IsClass)
                    {
                        throw new EpicSerializerException(String.Format("{0}.{1} is a complex type marked as EpicRepeat, but is not an IEnumerable<T> where T : class.", mi.DeclaringType.Name, mi.Name));
                    }

                    var innerSerializable = innerEnumerableType.GetCustomAttribute<EpicSerializableAttribute>();
                    if (innerSerializable == null)
                    {
                        throw new EpicSerializerException(String.Format("{0}.{1} is marked EpicRepeat, but {2} is not marked with EpicSerializable.", mi.DeclaringType.Name, mi.Name, innerEnumerableType.Name));
                    }

                    Value = GetComplexRepeatFunc(innerEnumerableType);
                }
                else
                {
                    // primitive type
                    Value = GetRepeatFunc(innerEnumerableType);
                }

                Field = repeatAttribute.Field;
                OmitIfEmpty = repeatAttribute.OmitIfEmpty;
            }
        }

        private static bool EnumerableOfPrimitives(Type type)
        {
            if (type == typeof(string))
            {
                return false;
            }

            foreach (var i in type.GetInterfaces())
            {
                if (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    var gArgs = i.GetGenericArguments()[0];
                    return EpicRepeatAttribute.ValidTypes.Contains(gArgs);
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the type T in an IEnumerable&lt;T&gt; or null if type does not implement IEnumerable&lt;&gt;.
        /// </summary>
        /// <param name="type">Any type</param>
        /// <returns>Type T in an IEnumerable&lt;T&gt; or null if type does not implement IEnumerable&lt;&gt;.</returns>
        private static Type GetEnumerableType(Type type)
        {
            if (type == typeof(string))
            {
                return null;
            }

            foreach (Type gType in type.GetInterfaces())
            {
                if (gType.IsGenericType
                    && gType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return gType.GetGenericArguments()[0];
                }
            }

            return null;
        }

        /// <summary>
        /// Produces the converter to deal with single primitive type members.
        /// </summary>
        /// <param name="t"> member type</param>
        /// <returns>object -&gt; string</returns>
        private Func<object, string> GetValueFunc(Type t)
        {
            // if type not found in the map, we dun goofed
            if (!TypeMap.Var.TryGetValue(t, out Func<object, string> converter))
            {
                throw new EpicSerializerException(String.Format("EpicRecordAttribute.ValidTypes type {0} is missing from conversion generator.", t.Name));
            }

            // wrap and set
            string transform(object o)
            {
                var s = converter(o);
                if (String.IsNullOrWhiteSpace(s) && OmitIfEmpty)
                {
                    return null;
                }
                return String.Format("{0},{1}", Field, s != null ? s : "");
            };

            return transform;
        }

        /// <summary>
        /// Produces the converter to deal with IEnumerable&lt;&gt; primitive type members.
        /// </summary>
        /// <param name="t">Any type</param>
        /// <returns>object -&gt; string</returns>
        private Func<object, string> GetRepeatFunc(Type t)
        {
            if (!TypeMap.Iter.TryGetValue(t, out Func<object, IEnumerable<string>> converter))
            {
                throw new EpicSerializerException(String.Format("EpicRepeatAttribute.ValidTypes type {0} is missing from conversion generator.", t.Name));
            }

            string transform(object o)
            {
                var iter = converter(o);
                if (iter == null || iter.Count() == 0)
                {
                    if (OmitIfEmpty)
                    {
                        return null;
                    }
                    return String.Format("{0},", Field);
                }

                return String.Join("\r\n", iter.Select(s => String.Format("{0},{1}", Field, s != null ? s : "")));
            };

            return transform;
        }

        /// <summary>
        /// Produces the converter for dealing with IEnumerable&lt;T&gt; where T : class.
        /// </summary>
        /// <param name="t">Any type</param>
        /// <returns>object -&gt; string</returns>
        private Func<object, string> GetComplexRepeatFunc(Type t)
        {
            IEnumerable<string> converter (object o)
            {
                var iter = (IEnumerable<object>)o;
                using (var epic = new EpicSerializerImpl(t))
                {
                    return epic.Serialize(iter);
                }
            };

            string transform(object obj)
            {
                var iter = converter(obj);
                return String.Join("\r\n", iter);
            }

            return transform;
        }
    }
}
