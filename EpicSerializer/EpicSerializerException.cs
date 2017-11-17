using System;

namespace EpicSerializer
{
    /// <summary>
    /// Indicates that the EpicSerializer attributes have been omitted or applied incorrectly on the type passed to the serializer.
    /// </summary>
    public class EpicSerializerException : Exception
    {
        /// <summary>
        /// Throwable constructor.
        /// </summary>
        /// <param name="message"><see cref="Exception.Message"/></param>
        public EpicSerializerException(string message) : base(message) { }
    }
}
