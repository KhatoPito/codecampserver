using System;
using System.Web.Mvc;

namespace July09v31.UI
{
    public interface ISubController : IController
    {
        Action GetResult(ControllerBase parentController);
    }
}