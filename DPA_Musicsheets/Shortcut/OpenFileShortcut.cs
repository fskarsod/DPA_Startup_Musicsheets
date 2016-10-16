using DPA_Musicsheets.Command;

namespace DPA_Musicsheets.Shortcut
{
    public class OpenFileShortcut : BaseCommandShortcut<IOpenFileCommand>
    {
        public override string Key => "OpenFile";

        public OpenFileShortcut(IOpenFileCommand command)
            : base(command)
        { }
    }
}
