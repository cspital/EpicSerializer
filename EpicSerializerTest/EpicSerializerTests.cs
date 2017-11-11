using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EpicSerializerTest.Model;
using EpicSerializer;
using System.Collections.Generic;

namespace EpicSerializerTest
{
    [TestClass]
    public class EpicSerializerTests
    {
        [ExpectedException(typeof(EpicSerializerException))]
        [TestMethod]
        public void ClassNothingTagged_ThrowNotSerializable()
        {
            var nt = new NothingTagged { Id = "hi" };

            try
            {
                var serializer = new EpicSerializer<NothingTagged>();
            } catch (EpicSerializerException e)
            {
                Assert.IsTrue(e.Message.EndsWith("is not EpicSerializable."));
                throw;
            }
        }

        [TestMethod]
        public void ClassNoTaggedMembers_ProduceEmptyString()
        {
            var ntm = new NoTaggedMembers { Id = "hi" };
            var serial = new EpicSerializer<NoTaggedMembers>();
            var result = serial.Serialize(ntm);

            Assert.AreEqual(String.Empty, result);
        }

        [TestMethod]
        public void ClassTaggedPrimitivesOnly_ProduceNormalOutput()
        {
            var tpo = new TaggedPrimitivesOnly { Id = "1234", Deactivated = new DateTime(2017, 10, 16) };
            var serial = new EpicSerializer<TaggedPrimitivesOnly>();
            var result = serial.Serialize(tpo);

            Assert.AreEqual("1,1234\r\n720,10/16/2017", result);
        }

        [TestMethod]
        public void ClassTaggedPrimitiveEnumerables_ProduceNormalOutput()
        {
            var tpe = new TaggedPrimitiveEnumerables
            {
                Id = "1234",
                Addresses = new List<string>
                {
                    "Test1",
                    "Test2"
                },
                Deactivated = new DateTime(2017, 10, 16)
            };
            var serial = new EpicSerializer<TaggedPrimitiveEnumerables>();
            var result = serial.Serialize(tpe);

            Assert.AreEqual("1,1234\r\n100,Test1\r\n100,Test2\r\n720,10/16/2017", result);
        }

        [ExpectedException(typeof(EpicSerializerException))]
        [TestMethod]
        public void ClassRecordMislabeledRepeat_ThrowNotIEnumerable()
        {
            try
            {
                var serial = new EpicSerializer<RecordMislabeledRepeat>();
            }
            catch (EpicSerializerException e)
            {
                Assert.IsTrue(e.Message.EndsWith("is marked EpicRepeat, but is not an IEnumerable<T>."));
                throw;
            }
        }

        [ExpectedException(typeof(EpicSerializerException))]
        [TestMethod]
        public void ClassRepeatMislabeledRecord_ThrowNotValidType()
        {
            try
            {
                var serial = new EpicSerializer<RepeatMislabeledRecord>();
            }
            catch (EpicSerializerException e)
            {
                Assert.IsTrue(e.Message.EndsWith("is marked as EpicRecord but is not in EpicRecordAttribute.ValidTypes."));
                throw;
            }
        }

        [ExpectedException(typeof(EpicSerializerException))]
        [TestMethod]
        public void ClassLabeledWithUnlabeledComplex_ThrowChildNotSerializable()
        {
            try
            {
                var serial = new EpicSerializer<LabeledWithUnlabeledComplex>();
            }
            catch (EpicSerializerException e)
            {
                Assert.IsTrue(e.Message.EndsWith("is not marked with EpicSerializable."));
                throw;
            }
        }

        [TestMethod]
        public void ClassLabeledWithComplex_ProduceNormalOutput()
        {
            var lwc = new LabeledWithComplex
            {
                Id = "1234",
                Repeater = new List<LabeledComplex>
                {
                    new LabeledComplex
                    {
                        State = "WA",
                        Code = "4321"
                    },
                    new LabeledComplex
                    {
                        State = "OR",
                        Code = "5432"
                    }
                }
            };
            var serial = new EpicSerializer<LabeledWithComplex>();
            var result = serial.Serialize(lwc);

            Assert.AreEqual("1,1234\r\n2300,WA\r\n2301,4321\r\n2300,OR\r\n2301,5432", result);
        }

        [TestMethod]
        public void ClassExtended_ProduceNormalOutput()
        {
            var ex = new Extended
            {
                Id = "1234",
                State = "WA",
                Code = "1234567"
            };

            var serial = new EpicSerializer<Extended>();
            var result = serial.Serialize(ex);

            Assert.AreEqual("1,1234\r\n2300,WA\r\n2301,1234567", result);
        }
    }
}
