using AutoMapper;
using July09v31.Core.Common;

namespace July09v31.UI.Models.CustomResolvers
{
    public class SpanWrappingFormatter : IValueFormatter
    {
        #region IValueFormatter Members

        public string FormatValue(ResolutionContext context)
        {
            string camelCaseMemberName = context.MemberName.ToLowerCamelCase();
            return string.Format(@"<span class=""{0}"">{1}</span>", camelCaseMemberName, context.SourceValue);
        }

        #endregion
    }
}