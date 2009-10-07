using AutoMapper;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.ObjectMapping.ConfigurationProfiles;

namespace CodeCampServer.Infrastructure.ObjectMapping
{
	public class AutoMapperConfiguration : IRequiresConfigurationOnStartup
	{
		public static void Configure()
		{
			Mapper.Initialize(x =>
			                  	{
			                  		x.ConstructTypeConvertersUsing(type => DependencyRegistrar.Resolve(type));
			                  		x.AddProfile<AutoMapperProfile>();
			                  		x.AddProfile<MeetingMapperProfile>();
			                  		x.AddProfile<UserGroupMapperProfile>();
			                  		x.AddProfile<MeetingMessageMapperProfile>();
									x.AddProfile<UserMapperProfile>();									
			                  	});
		}

		void IRequiresConfigurationOnStartup.Configure()
		{
			Configure();
		}
	}
}