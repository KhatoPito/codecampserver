using AutoMapper;
using July09v31.Core.Common;

namespace July09v31.UI.Models.CustomResolvers
{
    public class MoneyFormatter : IValueFormatter
    {
        public string FormatValue(ResolutionContext context)
        {
            if (context.SourceValue == null)
                return null;

            if (!(context.SourceValue is decimal))
                return context.SourceValue.ToNullSafeString();

            return ((decimal)context.SourceValue).ToString("c");
        }
    }
}