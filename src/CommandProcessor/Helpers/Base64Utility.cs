using System;
using System.IO;
using System.Text;

namespace Tarantino.RulesEngine
{
	public class Base64Utility : IBase64Utility
	{
		#region IBase64Utility Members

		public byte[] Decode(string encodedText)
		{
			byte[] DecodedData = Convert.FromBase64String(encodedText);
			return DecodedData;
		}

		public string Encode(Stream stream)
		{
			/*	About the 3KB block size:
			 *	Any block size divisible by 3 works all right because Convert.ToBase64String() method adds trailing '='s to the
			 *	last read base64Block if needed. Block of 3 bytes works ok also, but performance for big inputStreams
			 *	might not be good enough because of large number of calls.
			*/

			int BASE64_BLOCK_SIZE = 3*1000; // 3KB

			var base64Block = new byte[BASE64_BLOCK_SIZE];

			int bytesRead = 0;

			var sb = new StringBuilder();
			do
			{
				bytesRead = stream.Read(base64Block, 0, base64Block.Length);

				string base64String = Convert.ToBase64String(base64Block, 0, bytesRead);

				sb.Append(base64String);
			} while (bytesRead == base64Block.Length);

			return sb.ToString();
		}

		#endregion
	}
}