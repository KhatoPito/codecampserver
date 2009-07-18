using System;
using System.Linq.Expressions;
using July09v31.Core.Domain;
using July09v31.Core.Domain.Model;
using MvcContrib;

namespace July09v31.UI.Helpers.ViewPage.InputBuilders
{
    public class TimeSlotInputBuilder : PersistentObjectInputBuilder<TimeSlot>
    {
        private readonly ITimeSlotRepository _repository;

        public TimeSlotInputBuilder(ITimeSlotRepository repository)
        {
            _repository = repository;
        }


        protected override Expression<Func<TimeSlot, string>> GetDisplayPropertyExpression()
        {
            return t => t.StartTime + " to " + t.EndTime;
        }

        protected override TimeSlot[] GetList()
        {
            var conference = InputSpecification.Helper.ViewData.Get<Conference>();
            return _repository.GetAllForConference(conference);
        }
    }
}