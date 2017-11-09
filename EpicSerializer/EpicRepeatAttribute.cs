using System;
using System.Collections.Generic;

namespace EpicSerializer
{
    /// <summary>
    /// Marks a field or property as a repeating Epic record.
    /// Field or property must be an IEnumerable&lt;T&gt; where T: struct or class (if marked EpicSerializable).
    /// e.g. List&lt;string&gt; or List&lt;int&gt;
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class EpicRepeatAttribute : Attribute
    {
        /// <summary>
        /// Collection of primitive types allowed to be serialized.
        /// </summary>
        public static ICollection<Type> ValidTypes => TypeMap.Iter.Keys;

        /// <summary>
        /// Epic Master File field number.
        /// </summary>
        public int Field { get; }

        /// <summary>
        /// Indicates if field should be omitted if missing a value.
        /// </summary>
        public bool OmitIfEmpty { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Field"><see cref="EpicRecordAttribute.Field"/></param>
        public EpicRepeatAttribute(int Field) : this(false, Field) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Field"><see cref="EpicRecordAttribute.Field"/></param>
        /// <param name="OmitIfEmpty"><see cref="EpicRecordAttribute.OmitIfEmpty"/></param>
        public EpicRepeatAttribute(bool OmitIfEmpty, int Field)
        {
            this.Field = Field;
            this.OmitIfEmpty = OmitIfEmpty;
        }
    }
}
