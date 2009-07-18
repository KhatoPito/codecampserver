using System;
using System.Linq.Expressions;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using MvcContrib;

namespace July09v31.UI.Helpers.ViewPage.InputBuilders
{
    public class SpeakerInputBuilder : PersistentObjectInputBuilder<Speaker>
    {
        private readonly ISpeakerRepository _repository;

        public SpeakerInputBuilder(ISpeakerRepository repository)
        {
            _repository = repository;
        }


        protected override Expression<Func<Speaker, string>> GetDisplayPropertyExpression()
        {
            return t => t.FirstName + " " + t.LastName;
        }

        protected override Speaker[] GetList()
        {
            return _repository.GetAllForConference(base.InputSpecification.Helper.ViewData.Get<Conference>());
        }
    }
}