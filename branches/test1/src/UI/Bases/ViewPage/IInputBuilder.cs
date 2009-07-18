namespace July09v31.UI.Helpers.ViewPage
{
    public interface IInputBuilder
    {
        string Build(IInputSpecification specification);
        bool IsSatisfiedBy(IInputSpecification specification);
    }
}