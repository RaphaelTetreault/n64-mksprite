using CommandLine;

namespace MakeSprite
{
    public class Options
    {
        private static class Help
        {
            public const string Verbose =
                "Set output to verbose messages.";

            public const string InputPath =
                "Input path to source image file.";

            public const string OutputPath =
                "Output path to source image file.";

            public const string SearchSubdirectories =
                "(true|false) Whether or not to search subdirectories for files when using the directory mode.";

            public const string SearchPattern =
                "The search pattern used to find files. Ex: \"*.png\"";

            public const string Mode =
                "The mode of operation for this tool.";

            public const string ex = $"{SearchPattern}";

            public static readonly string FormatRO =
                $"The desired output sprite format." +
                $"\n\t{(byte)MakeSprite.Format.RGBA32} - {MakeSprite.Format.RGBA32}" +
                $"\n\t{(byte)MakeSprite.Format.RGBA16} - {MakeSprite.Format.RGBA16}" +
                $"\n\t{(byte)MakeSprite.Format.CI8} - {MakeSprite.Format.CI8}" +
                $"\n\t{(byte)MakeSprite.Format.I8} - {MakeSprite.Format.I8}" +
                $"\n\t{(byte)MakeSprite.Format.IA8} - {MakeSprite.Format.IA8}" +
                $"\n\t{(byte)MakeSprite.Format.CI4} - {MakeSprite.Format.CI4}" +
                $"\n\t{(byte)MakeSprite.Format.I4} - {MakeSprite.Format.I4}" +
                $"\n\t{(byte)MakeSprite.Format.IA4} - {MakeSprite.Format.IA4}";

            public const string Format =
                $"The desired output sprite format." +
                $"\n\t1 - RGBA32" +
                $"\n\t2 - RGBA16" +
                $"\n\t3 - CI8" +
                $"\n\t4 - I8" +
                $"\n\t5 - IA8" +
                $"\n\t6 - CI4" +
                $"\n\t7 - I4" +
                $"\n\t8 - IA4";

            public const string SlicesH =
                "Number of slices across the horizontal axis (vertical cuts).";

            public const string SlicesV =
                "Number of slices across the vertical axis (horizontal cuts).";

            public const string ResizeW =
                "Resize image width";

            public const string ResizeH =
                "Resize image height";
        }


        [Option('v', "verbose", Required = false, HelpText = Help.Verbose)]
        public bool Verbose { get; set; }

        [Option('i', "inputPath", Required = true, HelpText = Help.InputPath)]
        public string InputPath { get; set; } = string.Empty;

        [Option('o', "outputPath", Required = false, HelpText = Help.OutputPath)]
        public string OutputPath { get; set; } = string.Empty;


        [Option("searchSubdirs", Required = false, HelpText = Help.SearchSubdirectories)]
        public bool SearchSubdirectories { get; set; }

        [Option("searchPattern", Required = false, HelpText = Help.SearchPattern)]
        public string SearchPattern { get; set; } = string.Empty;

        [Option('m', "mode", Required = false, HelpText = Help.Mode)]
        public OperationMode Mode { get; set; }

        [Option('f', "format", Required = false, HelpText = Help.Format)]
        public Format Format { get; set; } = Format.None;

        [Option("slicesH", Required = false, HelpText = Help.SlicesH)]
        public int SlicesH { get; set; } = 1;

        [Option("slicesV", Required = false, HelpText = Help.SlicesV)]
        public int SlicesV { get; set; } = 1;


        [Option("resizeW", Required = false, HelpText = Help.ResizeW)]
        public int? ResizeW { get; set; }

        [Option("resizeH", Required = false, HelpText = Help.ResizeH)]
        public int? ResizeH { get; set; }


        public SearchOption SearchOption => SearchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        public bool UserWantsResize => ResizeW != null && ResizeH != null;


        public void PrintState()
        {
            Console.WriteLine("Options:");
            Console.WriteLine($"{nameof(Verbose)}: {Verbose}");
            Console.WriteLine($"{nameof(InputPath)}: {InputPath}");
            Console.WriteLine($"{nameof(OutputPath)}: {OutputPath}");
            Console.WriteLine($"{nameof(Mode)}: {Mode}");
            Console.WriteLine($"{nameof(SearchSubdirectories)}: {SearchSubdirectories}");
            Console.WriteLine($"{nameof(SearchOption)}: {SearchOption}");
            Console.WriteLine($"{nameof(SearchPattern)}: {SearchPattern}");
            Console.WriteLine($"{nameof(Format)}: {Format}");
            Console.WriteLine($"{nameof(SlicesH)}: {SlicesH}");
            Console.WriteLine($"{nameof(SlicesV)}: {SlicesV}");
            Console.WriteLine($"{nameof(ResizeW)}: {ResizeW}");
            Console.WriteLine($"{nameof(ResizeH)}: {ResizeH}");
        }
    }

}
