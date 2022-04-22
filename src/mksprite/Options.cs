using CommandLine;

namespace MakeSprite
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Option('p', "path", Required = true, HelpText = "Path to source image file.")]
        public string Path { get; set; } = string.Empty;

        [Option("searchSubdirs", Required = false, HelpText = "(true|false) Whether or not to search subdirectories for files when using the directory mode.")]
        public bool SearchSubdirectories { get; set; }

        [Option("searchPattern", Required = false, HelpText = "x.")]
        public string SearchPattern { get; set; } = string.Empty;

        [Option('m', "mode", Required = false, HelpText = "x")]
        public OperationMode Mode { get; set; }

        [Option('f', "format", Required = false, HelpText = "x")]
        public Format Format { get; set; } = Format.FMT_NONE;

        [Option("slicesH", Required = false, HelpText = "Number of slices across the horizontal axis (vertical cuts).")]
        public int SlicesH { get; set; } = 1;

        [Option("slicesV", Required = false, HelpText = "Number of slices across the vertical axis (horizontal cuts).")]
        public int SlicesV { get; set; } = 1;


        [Option("resizeW", Required = false, HelpText = "Resize image width")]
        public int? ResizeW { get; set; }

        [Option("resizeH", Required = false, HelpText = "Resize image height")]
        public int? ResizeH { get; set; }


        public SearchOption SearchOption => SearchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;


        public void PrintState()
        {
            Console.WriteLine("Options:");
            Console.WriteLine($"{nameof(Path)}: {Path}");
            Console.WriteLine($"{nameof(Verbose)}: {Verbose}");
            Console.WriteLine($"{nameof(Mode)}: {Mode}");
            Console.WriteLine($"{nameof(SearchSubdirectories)}: {SearchSubdirectories}");
            Console.WriteLine($"{nameof(SearchPattern)}: {SearchPattern}");
            Console.WriteLine($"{nameof(Format)}: {Format}");
        }
    }

}
