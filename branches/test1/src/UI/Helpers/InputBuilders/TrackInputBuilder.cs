using System;
using System.Linq.Expressions;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using MvcContrib;

namespace July09v31.UI.Helpers.ViewPage.InputBuilders
{
    public class TrackInputBuilder : PersistentObjectInputBuilder<Track>
    {
        private readonly ITrackRepository _repository;

        public TrackInputBuilder(ITrackRepository repository)
        {
            _repository = repository;
        }


        protected override Expression<Func<Track, string>> GetDisplayPropertyExpression()
        {
            return t => t.Name;
        }

        protected override Track[] GetList()
        {
            var conference = InputSpecification.Helper.ViewData.Get<Conference>();
            return _repository.GetAllForConference(conference);
        }
    }
}
