using EpicSerializer;
using System;

namespace EpicSerializerTest.Model
{
    [EpicSerializable(MasterFile.EMP)]
    public class Base
    {
        [EpicRecord(Field: 2300)]
        public string State { get; set; }

        [EpicRecord(Field: 2301)]
        public string Code { get; set; }
    }
}
