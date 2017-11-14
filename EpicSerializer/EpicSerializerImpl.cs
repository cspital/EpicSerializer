using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Text;
using System.Reflection;

namespace EpicSerializer
{
    internal class EpicSerializerImpl : IDisposable
    {
        private readonly static ConcurrentDictionary<Type, IEnumerable<MemberAccess>> AccessMap = new ConcurrentDictionary<Type, IEnumerable<MemberAccess>>();

        private IEnumerable<MemberAccess> Access { get; }
        private Type SerialType { get; }
        protected internal MasterFile File { get; }

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="t">Type for which a serializer is being built.</param>
        internal EpicSerializerImpl(Type t)
        {
            SerialType = t;
            var epicSerialAttr = SerialType.GetCustomAttribute<EpicSerializableAttribute>(true);
            if (epicSerialAttr == null)
            {
                throw new EpicSerializerException(String.Format("{0} is not EpicSerializable.", SerialType.Name));
            }
            File = epicSerialAttr.MasterFile;
            Access = AccessMap.GetOrAdd(SerialType, MakeInstructions);
        }

        /// <summary>
        /// Convert a single T into an Epic Chronicles string format.
        /// </summary>
        /// <param name="record">Object to serialize</param>
        /// <returns>The converted record.</returns>
        internal string Convert(object record)
        {
            var lines = new List<string>();
            foreach (var ma in Access)
            {
                object o = null;
                switch (ma.MemberType)
                {
                    case MemberTypes.Field:
                        o = SerialType.GetField(ma.Name).GetValue(record);
                        break;
                    case MemberTypes.Property:
                        o = SerialType.GetProperty(ma.Name).GetValue(record);
                        break;
                    default:
                        break;
                }

                if (ma.OmitIfEmpty && o == null)
                {
                    continue;
                }

                var chronLine = ma.Value(o);
                if (!String.IsNullOrWhiteSpace(chronLine))
                {
                    lines.Add(chronLine);
                }
            }
            return String.Join("\r\n", lines);
        }

        /// <summary>
        /// Convert an IEnumerable&lt;T&gt; into IEnumerable&lt;string&gt; Epic Chronicles string format.
        /// </summary>
        /// <param name="records">Objects to serialize</param>
        /// <returns>IEnumerable of converted records.</returns>
        internal IEnumerable<string> Serialize(IEnumerable<object> records)
        {
            foreach (var record in records)
            {
                yield return Convert(record);
            }
        }

        /// <summary>
        /// Generates a playbook for serializing the given type.
        /// </summary>
        /// <param name="t">The Type being serialized.</param>
        /// <returns>Steps to take when serializing this type.</returns>
        private static IEnumerable<MemberAccess> MakeInstructions(Type t)
        {
            var memberAccess = new List<MemberAccess>();
            var members = t
                .GetMembers()
                .Where(v =>v.GetCustomAttributes().Any(a => a is EpicRepeatAttribute || a is EpicRecordAttribute));
            
            foreach (var member in members)
            {
                switch (member.MemberType)
                {
                    case MemberTypes.Field:
                        memberAccess.Add(new MemberAccess((FieldInfo)member));
                        break;
                    case MemberTypes.Property:
                        memberAccess.Add(new MemberAccess((PropertyInfo)member));
                        break;
                    default:
                        continue;
                }
            }

            return memberAccess.OrderBy(m => m.Field);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void Dispose() { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
