using July09v31.UI.Helpers.ViewPage;

namespace July09v31.UI.Helpers.ViewPage
{
    public interface IInputBuilderFactory
    {
        IInputBuilder FindInputBuilderFor(IInputSpecification inputSpecification);
    }
}