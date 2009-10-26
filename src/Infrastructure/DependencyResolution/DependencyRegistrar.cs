using System;
using System.Web.Mvc;
using CodeCampServer.Infrastructure.Database;
using CodeCampServer.Infrastructure.UI.Services;
using CodeCampServer.UI.Helpers.Binders;
using CodeCampServer.UI.Views;
using StructureMap;

namespace CodeCampServer.DependencyResolution
{
	public class DependencyRegistrar
	{
		private static bool _dependenciesRegistered;
		private static readonly object sync = new object();

		public void RegisterDependencies()
		{
			//Logger.Debug(this, "Registering types with StructureMap");

			ObjectFactory.Initialize(x =>
			                         	{
			                         		x.Scan(y =>
			                         		       	{
			                         		       		y.AssemblyContainingType<DependencyRegistry>();
			                         		       		y.LookForRegistries();
			                         		       	});
			                         		x.AddRegistry<CastleValidatorRegistry>();
			                         	});
		}

		public static T Resolve<T>()
		{
			return ObjectFactory.GetInstance<T>();
		}

		public static object Resolve(Type modelType)
		{
			return ObjectFactory.GetInstance(modelType);
		}

		public static bool Registered<T>()
		{
			EnsureDependenciesRegistered();
			return ObjectFactory.GetInstance<T>() != null;
		}

		public static bool Registered(Type type)
		{
			EnsureDependenciesRegistered();
			return ObjectFactory.GetInstance(type) != null;
		}

		public static void EnsureDependenciesRegistered()
		{
			if (!_dependenciesRegistered)
			{
				lock (sync)
				{
					if (!_dependenciesRegistered)
					{
						ValidatiorRunnerFactory.Default=new ValidatiorRunnerFactory();
						
						new DependencyRegistrar().RegisterDependencies();

						AutoMapperConfiguration.Configure();
						//MvcContrib.UI.InputBuilder.InputBuilder.BootStrap();
						//			MvcContrib.UI.InputBuilder.InputBuilder.SetConventionProvider(() => new InputBuilderConventions());
						ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
						ModelBinders.Binders.DefaultBinder = new SmartBinder();

						ModelBinders.Binders.DefaultBinder = new SmartBinder();
						//DependencyRegistrar.EnsureDependenciesRegistered();
						//ModelBinders.Binders.Add(typeof (UserGroup),
						//                         DependencyRegistrar.Resolve<UserGroupModelBinder>());

						DatabaseCreator.Create();
						_dependenciesRegistered = true;
					}
				}
			}
		}
	}
}