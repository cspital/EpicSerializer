using System;

namespace EpicSerializer
{
    /// <summary>
    /// Document which Master File this class is destined for.
    /// </summary>
    public enum MasterFile
    {
        /// <summary>
        /// Schedulable Epic Resource Master File.
        /// </summary>
        SER = 1,

        /// <summary>
        /// Epic User Master File.
        /// </summary>
        EMP,
    }

    /// <summary>
    /// Mark your class to be serialized to Epic Chronicles format.
    /// Specify the MasterFile destination.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EpicSerializableAttribute : Attribute
    {
        /// <summary>
        /// MasterFile destination.
        /// </summary>
        public MasterFile MasterFile { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="master"><see cref="EpicSerializableAttribute.MasterFile"/></param>
        public EpicSerializableAttribute(MasterFile master)
        {
            MasterFile = master;
        }
    }
}
