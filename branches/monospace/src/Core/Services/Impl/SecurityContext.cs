using System;
using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services.Impl
{
	public class SecurityContext : ISecurityContext
	{

		public bool IsAdmin()
		{
			return true;
			//User user = _session.GetCurrentUser();
			//UserGroup defaultUserGroup = _repository.GetDefaultUserGroup();
			//if (defaultUserGroup.GetUsers().Any(user1 => user1.Username == user.Username))
			//{
			//    return true;
			//}
			//return false;
		}
	}
}