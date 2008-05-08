using System;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Model
{
	[TestFixture]
	public class ConferenceServiceTester
	{
	    private MockRepository _mocks;	    
	    private IConferenceRepository _conferenceRepository;
	    private IPersonRepository _personRepository;
	    private ILoginService _loginService;
	    private ICryptoUtil _cryptoUtil;

	    [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();			
			_conferenceRepository = _mocks.CreateMock<IConferenceRepository>();
		    _personRepository = _mocks.CreateMock<IPersonRepository>();
	        _loginService = _mocks.DynamicMock<ILoginService>();
	        _cryptoUtil = _mocks.DynamicMock<ICryptoUtil>();
        }

        private IConferenceService getService(IClock clock)
        {
            return new ConferenceService(_conferenceRepository, _cryptoUtil, clock);            
        }
        
	    [Test]
	    public void CurrentConferenceShouldGetNextUpcomingConferenceIfThereIsOneOtherwiseMostRecent()
	    {	        
            Conference oldConference = new Conference("old-conf", "old conference");
            oldConference.StartDate = new DateTime(2007, 7, 1);
	        Conference nextConference = new Conference("new-conf", "new conference");
            oldConference.StartDate = new DateTime(2008, 10, 1);

            Expect.Call(_conferenceRepository.GetFirstConferenceAfterDate(new DateTime())).IgnoreArguments()
                .Return(null);
            Expect.Call(_conferenceRepository.GetMostRecentConference(new DateTime())).IgnoreArguments()
                .Return(oldConference);
	        Expect.Call(_conferenceRepository.GetFirstConferenceAfterDate(new DateTime())).IgnoreArguments()
	            .Return(nextConference);

            _mocks.ReplayAll();

            ClockStub clockStub = new ClockStub(new DateTime(2008, 2, 15));
	        IConferenceService service = getService(clockStub);
	        
            //first one should not have a future conference
            Conference conf = service.GetCurrentConference();
            Assert.That(conf, Is.EqualTo(oldConference));

            //this one should have a future conference
            conf = service.GetCurrentConference();
            Assert.That(conf, Is.EqualTo(nextConference));

            _mocks.VerifyAll();
	    }      
	}
}
