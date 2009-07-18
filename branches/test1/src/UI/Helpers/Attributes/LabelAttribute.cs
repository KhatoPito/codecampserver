using System;

namespace July09v31.UI.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class LabelAttribute : Attribute
    {
        private readonly string _value;

        public LabelAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
}