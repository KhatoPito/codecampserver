using System;
using System.Web.Mvc;
using CodeCampServer.Core.Services.Impl;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers;
using CodeCampServer.UI.Helpers.Mappers;
using StructureMap;

namespace CodeCampServer.DependencyResolution
{
	public class ControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(Type controllerType)
		{
			if(controllerType!=null)
			{
				return (IController) ObjectFactory.GetInstance(controllerType);
				//return (IController)new UserController(new UserRepository(new HybridSessionBuilder()), new UserMapper(new UserRepository(new HybridSessionBuilder()),new Cryptographer()), new SecurityContext(),new UserSession(new UserRepository(new HybridSessionBuilder())));
			}
			
			return null;
		}
	}
}