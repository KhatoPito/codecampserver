using System;
using System.Text;
using System.Web.Mvc.Html;
using July09v31.Core.Common;
using July09v31.UI.Helpers.Attributes;

namespace July09v31.UI.Helpers.ViewPage.InputBuilders
{
    public class HiddenInputBuilder : BaseInputBuilder
    {
        protected override string CreateInputElementBase()
        {
            return InputSpecification.Helper.Hidden(InputSpecification.InputName);
        }

        protected override void AppendLabel(StringBuilder output)
        {
            return;
        }

        public override bool IsSatisfiedBy(IInputSpecification specification)
        {
            return specification.PropertyInfo.HasCustomAttribute<HiddenAttribute>() ||
                   specification.PropertyInfo.PropertyType == typeof(Guid) || specification.PropertyInfo.PropertyType == typeof(Guid?);
        }
    }
}