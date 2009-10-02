using System.IO;

namespace Tarantino.RulesEngine
{
	public interface IBase64Utility
	{
		byte[] Decode(string encodedText);
		string Encode(Stream stream);
	}
}