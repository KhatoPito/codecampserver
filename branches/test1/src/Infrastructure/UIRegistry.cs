using July09v31.UI.Helpers.ViewPage;
using July09v31.UI.Helpers.ViewPage.InputBuilders;
using StructureMap.Configuration.DSL;

namespace July09v31.Infrastructure.UI
{
    public class UIRegistry : Registry
    {
        protected override void configure()
        {
            ForRequestedType<IInputBuilderFactory>()
                .TheDefault.Is.OfConcreteType<InputBuilderFactory>()
                .TheArrayOf<IInputBuilder>()
                .Contains(x =>
                            {
                                x.OfConcreteType<NoInputBuilder>();
                                x.OfConcreteType<HiddenInputBuilder>();
                                x.OfConcreteType<CheckboxInputBuilder>();
                                x.OfConcreteType<YesNoRadioInputBuilder>();
                                x.OfConcreteType<RadioInputBuilder>();
                                x.OfConcreteType<DateInputBuilder>();
                                x.OfConcreteType<EnumerationInputBuilder>();
                                x.OfConcreteType<TextBoxInputBuilder>();
                                x.OfConcreteType<TrackInputBuilder>();
                                x.OfConcreteType<TimeSlotInputBuilder>();
                                x.OfConcreteType<SpeakerInputBuilder>();
                                x.OfConcreteType<UserInputBuilder>();
                            });
        }
    }
}