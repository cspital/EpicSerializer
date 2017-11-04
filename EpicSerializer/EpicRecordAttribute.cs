using System;

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
        public static Type[] ValidTypes { get; } = new Type[]
        {
            typeof(string),
            typeof(DateTime),
            typeof(DateTime?),
            typeof(short),
            typeof(short?),
            typeof(ushort),
            typeof(ushort?),
            typeof(int),
            typeof(int?),
            typeof(uint),
            typeof(uint?),
            typeof(long),
            typeof(long?),
            typeof(ulong),
            typeof(ulong?),
            typeof(float),
            typeof(float?),
            typeof(double),
            typeof(double?),
            typeof(decimal),
            typeof(decimal?),
            typeof(bool),
            typeof(bool?)
        };

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
