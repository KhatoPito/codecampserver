using System;
using System.Web.Mvc;
using StructureMap;

namespace July09v31.DependencyResolution
{
    public class ControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(Type controllerType)
        {
            if (controllerType != null)
            {
                return (IController)ObjectFactory.GetInstance(controllerType);
            }
            return null;
        }
    }
}