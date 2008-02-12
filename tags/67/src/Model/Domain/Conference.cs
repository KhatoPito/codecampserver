using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace CodeCampServer.Model.Domain
{
    public class Conference : EntityBase
    {
        private string _key;
        private string _name;
        private string _description;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private string _sponsorInfo;
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

        public virtual string Description
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

        public virtual string SponsorInfo
        {
            get { return _sponsorInfo; }
            set { _sponsorInfo = value; }
        }

        public virtual Location Location
        {
            get { return _location; }
        }

        public virtual int MaxAttendees
        {
            get { return _maxAttendees; }
            set { _maxAttendees = value; }
        }

        public virtual TimeSlot AddTimeSlot(DateTime startTime, DateTime endTime)
        {
            if(startTime < StartDate || endTime > EndDate)
            {
                throw new Exception("Time slot must be within conference.");
            }

            TimeSlot timeSlot = new TimeSlot(startTime, endTime);
            _timeSlots.Add(timeSlot);
            return timeSlot;
        }

        public virtual TimeSlot[] TimeSlots
        {
            get { return new List<TimeSlot>(_timeSlots).ToArray(); }
        }

        public virtual Sponsor[] GetSponsors()
        {
            Sponsor[] result = new List<Sponsor>(_sponsors).ToArray();
            Array.Sort(result, delegate(Sponsor x, Sponsor y) { return y.Level.CompareTo(x.Level); });
            return result;
        }

        public virtual Sponsor[] GetSponsors(SponsorLevel level)
        {
            List<Sponsor> sponsors = new List<Sponsor>(_sponsors);
            return sponsors.FindAll(delegate(Sponsor item) { return item.Level == level; }).ToArray();
        }

        public virtual void AddSponsor(Sponsor sponsor, SponsorLevel level)
        {
            sponsor.Level = level;
            if (!_sponsors.Contains(sponsor))
                _sponsors.Add(sponsor);
        }

        public override string ToString()
        {
            return Key;
        }
    }
}
