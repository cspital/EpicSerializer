using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpicSerializer;

namespace EpicSerializerTest.Model
{
    [EpicSerializable(MasterFile.EMP)]
    public class LabeledComplex
    {
        [EpicRecord(Field: 2300)]
        public string State { get; set; }

        [EpicRecord(Field: 2301)]
        public string Code { get; set; }
    }
}
