using System;

namespace Tarantino.RulesEngine.Mvc
{
	public class OriginalFormRetriever : IOriginalFormRetriever
	{
		public const string ORIGINAL_FORM_KEY = "ORIGINAL_FORM_KEY";
		private readonly ISerializer _serializer;
		private readonly IWebContext _webContext;

		public OriginalFormRetriever(IWebContext webContext, ISerializer serializer)
		{
			_webContext = webContext;
			_serializer = serializer;
		}

		#region IOriginalFormRetriever Members

		public T Retrieve<T>()
		{
			return (T) Retrieve(typeof (T));
		}

		public object Retrieve(Type formType)
		{
			string originalState = _webContext.GetRequestItem(ORIGINAL_FORM_KEY);
			if (originalState != null)
			{
				object deserialize = _serializer.Deserialize(formType, originalState);
				return deserialize;
			}
			return Activator.CreateInstance(formType);
		}

		#endregion
	}
}