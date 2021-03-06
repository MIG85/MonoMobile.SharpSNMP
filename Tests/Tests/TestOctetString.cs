/*
 * Created by SharpDevelop.
 * User: lextm
 * Date: 2008/5/2
 * Time: 12:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using Lextm.SharpSnmpLib.Messaging;
using NUnit.Framework;
using System.Text;

#pragma warning disable 1591,0618
namespace Lextm.SharpSnmpLib.Tests
{
    [TestFixture]
    public class TestOctetString
    {
        [Test]
        public void TestMethod()
        {
            byte[] expected = new byte[] {0x04, 0x06, 0x70, 0x75, 0x62, 0x6C, 0x69, 0x63};
            ISnmpData data = DataFactory.CreateSnmpData(expected);
            Assert.AreEqual(SnmpType.OctetString, data.TypeCode);
            OctetString s = (OctetString)data;
            Assert.AreEqual("public", s.ToString());
        }
        
        [Test]
        public void TestEqual()
        {
            Assert.AreEqual(new OctetString("public"), new OctetString("public"));
        }
        
        [Test]
        public void TestToBytes()
        {
            Assert.AreEqual(2, new OctetString("").ToBytes().Length);
        }
        
        [Test]
        public void TestEmpty()
        {
            Assert.AreEqual("", OctetString.Empty.ToString());
        }
        
        [Test]
        public void TestChinese()
        {
            Assert.AreEqual("�й�", new OctetString("�й�", Encoding.Unicode).ToString(Encoding.Unicode));
        }
    }
}
#pragma warning restore 1591,0618