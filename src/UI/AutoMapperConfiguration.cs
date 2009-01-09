using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.AutoMap;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Models
{
	public class AutoMapperConfiguration
	{
		public static void Configure()
		{
			AutoMapper.InitializeFormatting(x =>
			                                	{
//                                                        x.AddFormatter<HtmlEncoderFormatter>();
//                                                        x.AddFormatter<SpanWrappingFormatter>();
//                                                        x.ForSourceType<DateTime>().AddFormatter<StandardDateTimeFormatter>();
//                                                        x.ForSourceType<bool>().AddFormatter<YesNoBooleanFormatter>();
//                                                        x.ForSourceType<DateTime?>().AddFormatter<StandardDateTimeFormatter>();
//                                                        x.ForSourceType<TimeSpan>().AddFormatter<TimeSpanFormatter>();
//                                                        x.ForSourceType<decimal>().AddFormatter<MoneyFormatter>();
//                                                        x.ForSourceType<decimal?>().AddFormatter<MoneyFormatter>();
//                                                        x.ForSourceType<float>().AddFormatter<AxisValueFormatter>();
//                                                        x.ForSourceType<float?>().AddFormatter<AxisValueFormatter>();
////														x.ForSourceType<Name>().AddFormatter(ObjectFactory.GetInstance<FullNameFormatter>());
			                                		//x.ForSourceType<Guid>().SkipFormatter<SpanWrappingFormatter>();
			                                	});


			AutoMapper.Configure<User, UserForm>()
				.ForDtoMember(u => u.Password, o => o.ResolveUsing(new NullValueResolver()));

			AutoMapper.Configure<Conference, ConferenceForm>();

			AutoMapper.Configure<Track, TrackForm>();
			AutoMapper.Configure<User, UserForm>().ForDtoMember(f => f.ConfirmPassword,
			                                                    o => o.ResolveUsing(new NullValueResolver()));

			AutoMapper.Configure<TimeSlot, TimeSlotForm>();
			AutoMapper.Configure<Speaker, SpeakerForm>();
		}
	}

	internal class NullValueResolver : IValueResolver
	{
		public object Resolve(object model)
		{
			return null;
		}
	}
}