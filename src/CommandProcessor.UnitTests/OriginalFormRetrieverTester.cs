using System.Net.Mail;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.Mvc;

namespace Tarantino.UnitTests.UI
{
	[TestFixture]
	public class OriginalFormRetrieverTester : TestBase
	{
		[Test]
		public void Should_retrieve_from_web_context()
		{
			var context = S<IWebContext>();
			context.Stub(x => x.GetRequestItem(OriginalFormRetriever.ORIGINAL_FORM_KEY)).Return("foo");
			var serializer = S<ISerializer>();
			var message = new MailMessage();
			serializer.Stub(x => x.Deserialize(typeof (MailMessage), "foo")).Return(message);

			IOriginalFormRetriever retriever = new OriginalFormRetriever(context, serializer);

			var originalForm = retriever.Retrieve<MailMessage>();
			originalForm.ShouldBeTheSameAs(message);
		}
	}
}