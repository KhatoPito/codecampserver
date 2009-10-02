using System.IO;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Tarantino.RulesEngine;

namespace Tarantino.UnitTests.Core.Services
{
	[TestFixture]
	public class Base64UtilityTester
	{
		[Test]
		public void Can_decode_byte_array()
		{
			IBase64Utility utility = new Base64Utility();
			byte[] decodedData = utility.Decode("SkNNUw==");
			decodedData.ShouldEqual(new byte[] {74, 67, 77, 83});
		}

		[Test]
		public void Can_encode_using_stream_to_a_Base64_encoded_string()
		{
			var stream = new MemoryStream(new byte[] {74, 67, 77, 83});
			IBase64Utility utility = new Base64Utility();
			string encodedString = utility.Encode(stream);
			encodedString.ShouldEqual("SkNNUw==");
		}
	}
}