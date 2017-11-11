using EpicSerializer;
using System;

namespace EpicSerializerTest.Model
{
    [EpicSerializable(MasterFile.SER)]
    public class Extended : Base
    {
        [EpicRecord(Field: 1)]
        public string Id { get; set; }
    }
}
