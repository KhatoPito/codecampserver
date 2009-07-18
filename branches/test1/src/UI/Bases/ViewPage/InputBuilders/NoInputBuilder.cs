using July09v31.Core.Common;
using July09v31.UI.Helpers.Attributes;

namespace July09v31.UI.Helpers.ViewPage.InputBuilders
{
    public class NoInputBuilder : BaseInputBuilder
    {
        public override bool IsSatisfiedBy(IInputSpecification specification)
        {
            return specification.PropertyInfo.HasCustomAttribute<NoInputAttribute>();
        }

        protected override string CreateInputElementBase()
        {
            return (GetValue() ?? "").ToString();
        }
    }
}