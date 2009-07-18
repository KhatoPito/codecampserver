using System.Web;
using AutoMapper;

namespace July09v31.UI.Models.CustomResolvers
{
    public class HtmlEncoderFormatter : IValueFormatter
    {
        public string FormatValue(ResolutionContext context)
        {
            return HttpUtility.HtmlEncode(Core.Common.PrimitiveExtensions.ToNullSafeString(context.SourceValue));
        }
    }
}