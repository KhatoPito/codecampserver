using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tarantino.RulesEngine
{
	public class Serializer : ISerializer
	{
		private readonly IBase64Utility _base64Utility;

		public Serializer(IBase64Utility base64Utility)
		{
			_base64Utility = base64Utility;
		}

		#region ISerializer Members

		public string Serialize(object obj)
		{
			var stream = new MemoryStream();
			var formatter = new BinaryFormatter();
			formatter.Serialize(stream, obj);
			byte[] bytes = stream.ToArray();

			string encode = _base64Utility.Encode(new MemoryStream(bytes));
			return encode;
		}

		public object Deserialize(Type type, string str)
		{
			byte[] decoded = _base64Utility.Decode(str);
			var formatter = new BinaryFormatter();
			var stream = new MemoryStream(decoded);
			object deserialized = formatter.Deserialize(stream);
			return deserialized;
		}

		#endregion
	}
}