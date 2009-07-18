using System;
using System.Linq;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;

namespace July09v31.Infrastructure.DataAccess.Impl
{
    public class SpeakerRepository : KeyedRepository<Speaker>, ISpeakerRepository
    {
        public SpeakerRepository(ISessionBuilder sessionFactory)
            : base(sessionFactory)
        {
        }

        protected override string GetEntityNaturalKeyName()
        {
            return KEY_NAME;
        }

        public Speaker[] GetAllForConference(Conference conference)
        {
            Speaker[] list =
                GetSession().CreateQuery("from Speaker s where s.Conference = :conference").SetEntity("conference", conference).List
                    <Speaker>().ToArray();

            return list;
        }
    }
}