using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpicSerializer;

namespace EpicSerializerTest.Model
{
    [EpicSerializable(MasterFile.EMP)]
    public class LabeledWithComplex
    {
        [EpicRecord(Field: 1)]
        public string Id { get; set; }

        [EpicRepeat(Field: 2300)]
        public List<LabeledComplex> Repeater { get; set; }
    }
}
