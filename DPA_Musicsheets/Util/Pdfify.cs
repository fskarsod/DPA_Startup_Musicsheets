using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Util
{
    public interface IPdfify
    {
        void Save(string lilypond, string target);
    }

    public class Pdfify : IPdfify
    {
        private static readonly string LilypondLocation = @"D:\Program\LilyPond\usr\bin\lilypond.exe";

        public void Save(string source, string target)
        {
            var sourceFolder = Path.GetDirectoryName(source);

            var process = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = sourceFolder,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = $"--pdf \"{source}\"",
                    FileName = LilypondLocation
                }
            };

            process.Start();
            while (!process.HasExited)
            { /* Wait for exit */ }

            File.Move($"{target}.pdf" , target);
            File.Move($"{target}.mid", $"{target.Substring(0, target.Length - 4)}.mid");
        }
    }
}
