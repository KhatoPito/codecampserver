using System;
using System.Net.Mail;
using System.Security.Policy;
using System.Security.Principal;
using System.Xml;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Views
{
	[TestFixture]
	public class SmartBagTester
	{
		[Test]
		public void ShouldRetrieveSingleObjectByType()
		{
			SmartBag bag = new SmartBag();
			Url url = new Url("/asdf"); //arbitrary object
			bag.Add(url);

			Assert.That(bag.Get<Url>(), Is.EqualTo(url));
			Assert.That(bag.Get(typeof (Url)), Is.EqualTo(url));
		}

		[Test, ExpectedException(ExceptionType = typeof (ArgumentException),
			ExpectedMessage = "You can only add one default object for type 'System.Security.Policy.Url'.")]
		public void AddingTwoDefaultObjectsOfSameTypeThrows()
		{
			Url url1 = new Url("/1");
			Url url2 = new Url("/2");

			SmartBag bag = new SmartBag();
			bag.Add(url1);
			bag.Add(url2);
		}

		[Test, ExpectedException(typeof (ArgumentException),
			ExpectedMessage = "No object exists with key 'System.Security.Policy.Url'.")]
		public void ShouldGetMeaningfulExceptionIfObjectDoesntExist()
		{
			SmartBag bag = new SmartBag();
			Url url = bag.Get<Url>();
		}

		[Test]
		public void ShouldReportContainsCorrectly()
		{
			SmartBag bag = new SmartBag();
			bag.Add(new Url("/2"));

			Assert.That(bag.Contains<Url>());
			Assert.That(bag.Contains(typeof (Url)));
		}

		[Test]
		public void ShouldManageMoreThanOneObjectPerType()
		{
			SmartBag bag = new SmartBag();
			bag.Add("key1", new Url("/1"));
			bag.Add("key2", new Url("/2"));

			Assert.That(bag.Get<Url>("key1").Value, Is.EqualTo("/1"));
			Assert.That(bag.Get<Url>("key2").Value, Is.EqualTo("/2"));
		}

		[Test, ExpectedException(typeof (ArgumentException), 
			ExpectedMessage = "No object exists with key 'foobar'.")]
		public void ShouldGetMeaningfulExceptionIfObjectDoesntExistByKey()
		{
			SmartBag bag = new SmartBag();
			Url url = bag.Get<Url>("foobar");
		}

		[Test]
		public void ShouldCountNumberOfObjectsOfGivenType()
		{
			SmartBag bag = new SmartBag();
			Assert.That(bag.GetCount(typeof (Url)), Is.EqualTo(0));

			bag.Add("1", new Url("/1"));
			bag.Add("2", new Url("/2"));
			bag.Add("3", new Url("/3"));

			Assert.That(bag.GetCount(typeof (Url)), Is.EqualTo(3));
		}

		[Test]
		public void ShouldBeAbleToInitializeBagWithSeveralObjects()
		{
			Url url = new Url("/1");
			GenericIdentity identity = new GenericIdentity("name");

			SmartBag bag = new SmartBag().Add(identity).Add(url);
			Assert.That(bag.Get(typeof (GenericIdentity)), Is.EqualTo(identity));
			Assert.That(bag.Get(typeof (Url)), Is.EqualTo(url));
		}

	    [Test]
	    public void ShouldBeAbleToGetADefaultValueIfTheKeyDoesntExist()
	    {
	        DateTime theDate = DateTime.Parse("April 04, 2005");
	        DateTime defaultDate = DateTime.Parse("October 31, 2005");

            SmartBag bag = new SmartBag();	        
            Assert.That(bag.GetOrDefault("some_date", defaultDate), Is.EqualTo(defaultDate));           

            bag.Add("some_date", theDate);
            Assert.That(bag.GetOrDefault("some_date", defaultDate), Is.EqualTo(theDate));
	    }        

		[Test]
		public void ShouldHandleProxiedObjectsByType()
		{
			MailMessage stub = MockRepository.GenerateStub<MailMessage>();
			SmartBag bag = new SmartBag();
			bag.Add(stub);
			MailMessage message = bag.Get<MailMessage>();

			Assert.That(message, Is.EqualTo(stub));
		}

		[Test]
		public void ShouldInitializeWithProxiesAndResolveCorrectly()
		{
			MailMessage messageProxy = MockRepository.GenerateStub<MailMessage>();
			XmlDocument xmlDocumentProxy = MockRepository.GenerateStub<XmlDocument>();

			SmartBag bag = new SmartBag().Add(messageProxy).Add(xmlDocumentProxy);

			Assert.That(bag.Get<MailMessage>(), Is.EqualTo(messageProxy));
			Assert.That(bag.Get<XmlDocument>(), Is.EqualTo(xmlDocumentProxy));
		}

		[Test]
		public void ShouldInitializeWithKeys()
		{
			SmartBag bag = new SmartBag().Add("key1", 2).Add("key2", 3);
			Assert.That(bag.ContainsKey("key1"));
			Assert.That(bag.ContainsKey("key2"));
		}
	}
}
