using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Binders;
using NBehave.Spec.NUnit;

namespace CodeCampServer.IntegrationTests.DependencyResolution
{
	using NUnit.Framework;

	
		[TestFixture]
		public class DependencyRegistrarTester
		{

			[Test]
			public void Reflection_helper_can_resolve_repositories()
			{				
				ReflectionHelper.IsConcreteAssignableFromGeneric(typeof(SpeakerRepository), typeof(IKeyedRepository<>)).ShouldNotBeNull();
				ReflectionHelper.IsConcreteAssignableFromGeneric(typeof(SessionRepository), typeof(IKeyedRepository<>)).ShouldNotBeNull();
				ReflectionHelper.IsConcreteAssignableFromGeneric(typeof(ConferenceRepository), typeof(IKeyedRepository<>)).ShouldNotBeNull();
				ReflectionHelper.IsConcreteAssignableFromGeneric(typeof(TimeSlotRepository), typeof(IKeyedRepository<>)).ShouldBeNull();
				ReflectionHelper.IsConcreteAssignableFromGeneric(typeof(TrackRepository), typeof(IKeyedRepository<>)).ShouldBeNull();

				ReflectionHelper.IsConcreteAssignableFromGeneric(typeof(SpeakerRepository), typeof(IRepository<>)).ShouldNotBeNull();
				ReflectionHelper.IsConcreteAssignableFromGeneric(typeof(SessionRepository), typeof(IRepository<>)).ShouldNotBeNull();
				ReflectionHelper.IsConcreteAssignableFromGeneric(typeof(ConferenceRepository), typeof(IRepository<>)).ShouldNotBeNull();
				ReflectionHelper.IsConcreteAssignableFromGeneric(typeof(TimeSlotRepository), typeof(IRepository<>)).ShouldNotBeNull();
				ReflectionHelper.IsConcreteAssignableFromGeneric(typeof(TrackRepository), typeof(IRepository<>)).ShouldNotBeNull();
			}

			[Test]
			public void Should_register_all_objects()
			{
				DependencyRegistrar.EnsureDependenciesRegistered();
				IEnumerable<Type> controllers = GetControllers();
				foreach (Type controller in controllers)
				{
					DependencyRegistrar.Resolve(controller);
				}
			}

			[Test]
			public void Should_resolve_a_complex_type_for_the_Irepository()
			{
				DependencyRegistrar.EnsureDependenciesRegistered();

				Type repositoryType = typeof(IRepository<>).MakeGenericType(typeof(Conference));

				var binder = (IRepository<Conference>)DependencyRegistrar.Resolve(repositoryType);

				binder.ShouldBeAssignableFrom(typeof(ConferenceRepository));
			}

			[Test]
			public void Should_resolve_a_complex_type_for_the_IKeyed_repository()
			{
				DependencyRegistrar.EnsureDependenciesRegistered();
				Type repositoryType = typeof(IKeyedRepository<>).MakeGenericType(typeof(Conference));
				Type modelBinderType = typeof(KeyedModelBinder<,>).MakeGenericType(typeof(Conference), repositoryType);

				var binder = (IModelBinder)DependencyRegistrar.Resolve(modelBinderType);
				
				binder.ShouldNotBeNull();
			}

			private IEnumerable<Type> GetControllers() {
				Type[] types = typeof(HomeController).Assembly.GetTypes();
				return types.Where(e => IsAController(e));
			}

		private bool IsAController(Type e)
		{				
			return e.GetInterface("IController")!=null&& !e.IsAbstract;
		}
			
		}
	}