using DPA_Musicsheets.Command;

namespace DPA_Musicsheets.Shortcut
{
    public class PlayMidiShortcut : BaseCommandShortcut<IPlayCommand>
    {
        public override string Key => "PlayMidi";

        public PlayMidiShortcut(IPlayCommand command)
            : base(command)
        { }
    }
}
