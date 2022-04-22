// Used libraries / NuGet packages:
//      CommandLineParser
//      ImageSharp

using MakeSprite;
using Manifold;
using Manifold.IO;
using System;
//using System.Drawing;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace MakeSprite
{
    public static class Program
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

            [Option("slicesH", Min = 1, Required = false, HelpText = "Number of slices across the horizontal axis (vertical cuts).")]
            public int SlicesH { get; set; } = 1;

            [Option("slicesV", Min = 1, Required = false, HelpText = "Number of slices across the vertical axis (horizontal cuts).")]
            public int SlicesV { get; set; } = 1;


            [Option("resizeW", Required = false, HelpText = "Resize image width")]
            public int? ResizeW { get; set; }

            [Option("resizeH", Required = false, HelpText = "Resize image height")]
            public int? ResizeH{ get; set; }


            public SearchOption SearchOption => SearchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        }

        public static void Main(string[] args)
        {

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions);
        }

        public static void RunOptions(Options options)
        {
            Console.WriteLine("Options:");
            Console.WriteLine($"{nameof(options.Path)}: {options.Path}");
            Console.WriteLine($"{nameof(options.Verbose)}: {options.Verbose}");
            Console.WriteLine($"{nameof(options.Mode)}: {options.Mode}");
            Console.WriteLine($"{nameof(options.SearchSubdirectories)}: {options.SearchSubdirectories}");
            Console.WriteLine($"{nameof(options.SearchPattern)}: {options.SearchPattern}");
            Console.WriteLine($"{nameof(options.Format)}: {options.Format}");


            switch (options.Mode)
            {
                case OperationMode.interactive:
                    {
                        Console.WriteLine("Interactive mode");
                    }
                    break;
                case OperationMode.directory:
                    {
                        Console.WriteLine("Directory mode");
                        OpModeDirectory(options);
                    }
                    break;
                case OperationMode.file:
                    {
                        Console.WriteLine("File mode.");
                    }
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        public static void OpModeDirectory(Options options)
        {
            if (!Directory.Exists(options.Path))
            {
                Console.WriteLine($"Path provided is not a valid directory! Arg:{options.Path}");
            }

            var files = Directory.GetFiles(options.Path, options.SearchPattern, options.SearchOption);

            if (files.IsNullOrEmpty())
            {
                Console.WriteLine($"Pattern found no matches in path provided!");
                Console.WriteLine($"\t{nameof(options.Path)}:{options.Path}");
                Console.WriteLine($"\t{nameof(options.SearchPattern)}:{options.SearchPattern}");
                Console.WriteLine($"\t{nameof(options.SearchSubdirectories)}:{options.SearchSubdirectories}");
            }

            foreach (var file in files)
            {
                Console.WriteLine($"Processing file: {file}");

                var image = SixLabors.ImageSharp.Image.Load(file) as Image<Rgba32>;
                var sprite = new Sprite()
                {
                    FileName = Path.GetFileNameWithoutExtension(file),
                    Width = (ushort)image.Width,
                    Height = (ushort)image.Height,
                    Format = options.Format,
                    BitDepth = N64Encoding.FormatToBitsPerPixel(options.Format),
                    SlicesH = checked((byte)options.SlicesH),
                    SlicesV = checked((byte)options.SlicesV),
                };
                sprite.SetImage(image, options.Format);

                string dest = $"{Path.GetDirectoryName(file)}/{sprite.FileName}{sprite.FileExtension}";
                Console.Write($"Writing file: {dest}/{sprite.FileName}{sprite.FileExtension}");
                BinarySerializableIO.SaveFile(sprite, dest);
                Console.WriteLine($" ... success!");
            }
        }

        // TODO: implement resampler selection
        public static void TryResize(Image image, Options options)
        {
            bool doResizeW = options.ResizeW != null;
            bool doResizeH = options.ResizeH != null;
            if (doResizeW || doResizeH)
            {
                int w = doResizeW ? (int)options.ResizeW : image.Width;
                int h = doResizeH ? (int)options.ResizeH : image.Height;
                image.Mutate(ipc => ipc.Resize(w, h));
            }
        }
    }
}