using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpicSerializer;

namespace EpicSerializerTest.Model
{
    [EpicSerializable(MasterFile.EMP)]
    public class RecordMislabeledRepeat
    {
        [EpicRepeat(Field: 1)]
        public string Id { get; set; }
    }
}
