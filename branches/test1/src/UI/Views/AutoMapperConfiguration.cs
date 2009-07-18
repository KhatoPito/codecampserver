using AutoMapper;

namespace July09v31.UI.Views
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.AddProfile<AutoMapperProfile>());
        }
    }
}