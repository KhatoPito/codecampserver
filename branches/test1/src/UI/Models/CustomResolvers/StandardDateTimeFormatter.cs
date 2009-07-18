using System;
using AutoMapper;
using July09v31.Core.Common;


namespace July09v31.UI.Models.CustomResolvers
{
    public class StandardDateTimeFormatter : IValueFormatter
    {
        public string FormatValue(ResolutionContext context)
        {
            if (context.SourceValue == null)
                return null;

            if (!(context.SourceValue is DateTime))
                return context.SourceValue.ToNullSafeString();

            return ((DateTime)context.SourceValue).ToString("MM/dd/yyyy HH:mm");
        }
    }
}