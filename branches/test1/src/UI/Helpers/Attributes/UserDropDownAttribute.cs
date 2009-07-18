using System;
using July09v31.Core.Domain;

namespace July09v31.UI.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class UserDropDownAttribute : Attribute
    {
    }
}