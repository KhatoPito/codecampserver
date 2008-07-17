using CodeCampServer.Model;
using CodeCampServer.Website.Helpers;

namespace CodeCampServer.Website.Controllers
{
	public class FlashMessageComponent
	{
		private readonly IUserSession _session;

		public FlashMessageComponent() : this(IoC.Resolve<IUserSession>())
		{
		}

		public FlashMessageComponent(IUserSession session)
		{
			_session = session;
		}

		public void GetMessages()
		{
			FlashMessage[] flashMessages = _session.PopUserMessages();
            //TODO:  re-implement this without component controller
			//RenderView("list", flashMessages);
		}

	    public void RenderView(object o, object o1)
	    {	        
            //just to get this compiling without component controller
	    }
	}
}