using System;
using July09v31.Core;
using July09v31.UI.Helpers.Attributes;
using July09v31.UI.Models.Validation.Attributes;

namespace July09v31.UI.Models.Forms
{
    public class TimeSlotForm : ValueObject<TimeSlotForm>
    {
        public virtual Guid Id { get; set; }
        [BetterValidateDateTime("Start Time")]
        public virtual string StartTime { get; set; }
        [BetterValidateDateTime("End Time")]
        public virtual string EndTime { get; set; }
        public virtual Guid ConferenceId { get; set; }

        [Hidden]
        public virtual string ConferenceKey { get; set; }

        public string GetName()
        {
            string start = DateTime.Parse(StartTime).ToString("h:mm");
            string end = DateTime.Parse(EndTime).ToString("h:mm tt");
            return string.Format("{0} - {1}", start, end);
        }
    }
}