using System;
using System.Web.Mvc;
using StructureMap;

namespace CodeCampServer.DependencyResolution
{
	public class ControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(Type controllerType)
		{
			if(controllerType!=null)
			{
				return (IController)ObjectFactory.GetInstance(controllerType);
			}
			return null;
		}
	}
}