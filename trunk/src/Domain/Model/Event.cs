using System;

namespace CodeCampServer.Domain.Model
{
    public class Event : IEquatable<Event>
    {
        private Guid _id;
        private string _key;
        private string _name;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private string _sponsorInfoHtml;
        private Location _location = new Location();

        public Event()
        {
        }

        public Event(string key, string name)
        {
            _key = key;
            _name = name;
        }

        public virtual Guid Id
        {
            get { return _id; }
            set { _id = value; }
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

        public static Event Empty
        {
            get { return new Event(); }
        }

        public bool Equals(Event otherEvent)
        {
            if (otherEvent == null) return false;
            return Equals(Key, otherEvent.Key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Event);
        }

        public override int GetHashCode()
        {
            return Key != null ? Key.GetHashCode() : 0;
        }
    }
}