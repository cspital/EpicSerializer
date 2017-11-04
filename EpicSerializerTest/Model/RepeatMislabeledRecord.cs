using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpicSerializer;

namespace EpicSerializerTest.Model
{
    [EpicSerializable(MasterFile.EMP)]
    public class RepeatMislabeledRecord
    {
        [EpicRecord(Field: 1)]
        public string Id { get; set; }

        [EpicRecord(Field: 100)]
        public List<string> Addresses { get; set; }
    }
}
