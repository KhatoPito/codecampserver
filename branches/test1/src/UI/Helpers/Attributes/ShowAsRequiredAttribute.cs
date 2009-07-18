using System;

namespace July09v31.UI.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ShowAsRequiredAttribute : Attribute
    {
    }
}