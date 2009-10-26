using System;
using System.Web;
using StructureMap;

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
			DependencyRegistrar.EnsureDependenciesRegistered();
		}
	}
}