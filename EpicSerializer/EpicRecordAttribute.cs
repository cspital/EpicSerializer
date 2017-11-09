using System;
using System.Collections.Generic;

namespace EpicSerializer
{
    /// <summary>
    /// Marks a field or property as a one-to-one target.
    /// Field or property must be T where T : struct.
    /// e.g. string or int
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class EpicRecordAttribute : Attribute
    {
        /// <summary>
        /// Collection of primitive types allowed to be serialized.
        /// </summary>
        public static ICollection<Type> ValidTypes => TypeMap.Var.Keys;

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
        /// <param name="OmitIfEmpty"><see cref="EpicRecordAttribute.OmitIfEmpty"/></param>
        public EpicRecordAttribute(bool OmitIfEmpty, int Field)
        {
            this.Field = Field;
            this.OmitIfEmpty = OmitIfEmpty;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Field"><see cref="EpicRecordAttribute.Field"/></param>
        public EpicRecordAttribute(int Field) : this(false, Field) { }
    }
}
