using StructureMap;
using System.Collections.Generic;
namespace CodeCampServer.Model.Domain
{
	[PluginFamily(Keys.DEFAULT)]
	public interface ISpeakerRepository
	{
		void Save(Speaker speaker);

        IEnumerable<Speaker> GetSpeakersForConference(Conference conference, int pageNumber, int perPage);

        Speaker GetSpeakerByDisplayName(string displayName);
        Speaker GetSpeakerByEmail(string email);

        bool CanSaveSpeakerWithDisplayName(Speaker speaker, string newDisplayName);
    }
}
