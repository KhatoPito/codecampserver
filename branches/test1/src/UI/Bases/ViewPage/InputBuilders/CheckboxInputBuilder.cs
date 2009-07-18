using System.Web.Mvc.Html;

namespace July09v31.UI.Helpers.ViewPage.InputBuilders
{
    public class CheckboxInputBuilder : BaseInputBuilder
    {
        public override bool IsSatisfiedBy(IInputSpecification specification)
        {
            return specification.PropertyInfo.PropertyType == typeof(bool);
        }

        protected override string CreateInputElementBase()
        {
            return InputSpecification.Helper.CheckBox(InputSpecification.InputName, InputSpecification.CustomAttributes);
        }
    }
}