using AutoMapper;
using CodeCampServer.DependencyResolution;
using CodeCampServer.UI.Helpers.Mappers;

namespace CodeCampServer.UI.Views
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
			                  	});
		}

		void IRequiresConfigurationOnStartup.Configure()
		{
			Configure();
		}
	}
}