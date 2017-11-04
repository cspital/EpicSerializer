using System;
using System.Collections.Generic;

namespace EpicSerializer
{
    /// <summary>
    /// Generic EpicSerializer
    /// </summary>
    /// <typeparam name="T">Any type T where T : class</typeparam>
    public class EpicSerializer<T> : IDisposable
        where T : class
    {
        private EpicSerializerImpl Serializer { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EpicSerializer()
        {
            Serializer = new EpicSerializerImpl(typeof(T));
        }

        /// <summary>
        /// Serialize a single T.
        /// </summary>
        /// <param name="record">T to serialize</param>
        /// <returns>Epic Chronicles formatted string</returns>
        public string Serialize(T record)
        {
            return Serializer.Convert(record);
        }

        /// <summary>
        /// Serialize an IEnumerable of T.
        /// </summary>
        /// <param name="records">IEnumerable of T to serialize.</param>
        /// <returns>IEnumerable of Epic Chronicles formatted strings.</returns>
        public IEnumerable<string> Serialize(IEnumerable<T> records)
        {
            return Serializer.Serialize(records);
        }

        /// <summary>
        /// Serialize an IEnumerable of T.
        /// </summary>
        /// <param name="records">IEnumerable of T to serialize.</param>
        /// <returns>Eagerly built Epic Chronicles formatted string.</returns>
        public string EagerSerialize(IEnumerable<T> records)
        {
            return String.Join("", Serializer.Serialize(records));
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void Dispose() { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
