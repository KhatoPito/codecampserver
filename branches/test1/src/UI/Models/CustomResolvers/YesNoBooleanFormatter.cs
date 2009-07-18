using AutoMapper;
using July09v31.Core.Common;

namespace July09v31.UI.Models.CustomResolvers
{
    public class YesNoBooleanFormatter : IValueFormatter
    {
        public string FormatValue(ResolutionContext context)
        {
            if (context.SourceValue == null)
                return null;

            if (!(context.SourceValue is bool))
                return context.SourceValue.ToNullSafeString();

            return ((bool)context.SourceValue) ? "Yes" : "No";
        }
    }
}