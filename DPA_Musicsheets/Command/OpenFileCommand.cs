using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DPA_Musicsheets.Command
{
    public interface IOpenFileCommand : ICommand
    { }

    public class OpenFileCommand : BaseCommandWpf, IOpenFileCommand
    {
        private readonly IContentStorage _contentStorage;

        public OpenFileCommand(IContentStorage contentStorage)
        {
            _contentStorage = contentStorage;
        }

        public override void Execute(object parameter)
        {
            _contentStorage.Load();
        }
    }
}
