using System;
using Iesi.Collections.Generic;

namespace CodeCampServer.Domain.Model
{
    public class Conference : EntityBase
    {
        private string _key;
        private string _name;
        private string _description;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private string _sponsorInfoHtml;
        private Location _location = new Location();
        private int _maxAttendees;
        private ISet<TimeSlot> _timeSlots = new HashedSet<TimeSlot>();
        private ISet<Sponsor> _sponsors = new HashedSet<Sponsor>();

        public Conference()
        {
        }

        public Conference(string key, string name)
        {
            _key = key;
            _name = name;
        }

        public virtual string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public virtual DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public virtual DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public virtual string SponsorInfoHtml
        {
            get { return _sponsorInfoHtml; }
            set { _sponsorInfoHtml = value; }
        }

        public virtual Location Location
        {
            get { return _location; }
        }

        public int MaxAttendees
        {
            get { return _maxAttendees; }
            set { _maxAttendees = value; }
        }
    }
}