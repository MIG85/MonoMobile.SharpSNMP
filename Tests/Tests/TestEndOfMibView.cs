/*
 * Created by SharpDevelop.
 * User: lextm
 * Date: 2009/2/15
 * Time: 20:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using NUnit.Framework;
#pragma warning disable 1591, 0618
namespace Lextm.SharpSnmpLib.Tests
{
	/// <summary>
	/// Description of TestEndOfMibView.
	/// </summary>
	[TestFixture]
	public class TestEndOfMibView
	{
		[Test]
		public void TestToBytes()
		{
			EndOfMibView obj = new EndOfMibView();
			Assert.AreEqual(new byte[] { 0x82, 0x00 }, obj.ToBytes());
		}
	}
}
#pragma warning restore 1591, 0618