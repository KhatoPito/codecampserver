using System;
using System.Web;
using CodeCampServer.Core.Domain.Model;
using CommandProcessor;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using System.Linq;

namespace CodeCampServer.DependencyResolution
{
	public class DependencyRegistrarModule : IHttpModule
	{
		private static bool _dependenciesRegistered;
        private static object Lock = new object();

		public void Init(HttpApplication context)
		{
			context.BeginRequest += context_BeginRequest;
		}

		public void Dispose()
		{
		}

		private void context_BeginRequest(object sender, EventArgs e)
		{
			EnsureDependenciesRegistered();
		}

		private void EnsureDependenciesRegistered()
		{
			if (!_dependenciesRegistered)
			{
				lock (Lock)
				{
					if (!_dependenciesRegistered)
					{
						//Logger.Debug(this, "Registering types with StructureMap");
						ObjectFactory.ResetDefaults();
						var registrar = new DependencyRegistrar();
						registrar.RegisterDependencies();

						SetUpRulesEngine();

						_dependenciesRegistered = true;

						
					}
				}
			}
		}

		private void SetUpRulesEngine()
		{
			ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator());
			var rulesEngine = new RulesEngine();
			RulesEngine.MessageProcessorFactory = new MessageProcessorFactory();
			rulesEngine.Initialize(typeof(Event).Assembly);
		}
	}
}