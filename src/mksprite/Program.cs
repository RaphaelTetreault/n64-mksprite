// Used libraries / NuGet packages:
//      Mono.Options
//      System.Drawing.Common

using MakeSprite;
using Manifold;
using Manifold.IO;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using CommandLine;


public static class Program
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Option('p', "path", Required = true, HelpText = "Path to source image file.")]
        public string Path { get; set; } = string.Empty;

        [Option("searchSubdirs", Required = false, HelpText = "x")]
        public bool SearchSubdirectories { get; set; }

        [Option("searchPattern", Required = false, HelpText = "x.")]
        public string SearchPattern { get; set; } = string.Empty;

        [Option('m', "mode", Required = false, HelpText = "x")]
        public OperationMode Mode { get; set; }

        [Option('f', "format", Required = false, HelpText = "x")]
        public Format Format { get; set; } = Format.FMT_NONE;


        public SearchOption SearchOption => SearchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
    }

    public static void Main(string[] args)
    {
        string msg_searchOption = $"(true|false) Whether or not to search subdirectories for files when using the directory mode.";

        // See https://aka.ms/new-console-template for more information
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
            Console.WriteLine($"Pattern found no matches in Path provided! Arg:{options.Path}");
            Console.WriteLine($"\t{nameof(options.Path)}:{options.Path}");
            Console.WriteLine($"\t{nameof(options.SearchSubdirectories)}:{options.SearchSubdirectories}");
            Console.WriteLine($"\t{nameof(options.SearchPattern)}:{options.SearchPattern}");
        }


        foreach (var file in files)
        {
            Console.WriteLine($"Processing file: {file}");

            var bitmap = Bitmap.FromFile(file);
            var encoding = N64Encoding.FormatToEncoding(options.Format);
            var sprite = new Sprite()
            {
                FileName = Path.GetFileNameWithoutExtension(file),
                Width = (ushort)bitmap.Width,
                Height = (ushort)bitmap.Height,
                Format = options.Format,
                BitDepth = N64Encoding.FormatToBitsPerPixel(options.Format),
                SlicesH = 0,
                SlicesV = 0,
            };

            string dest = $"{Path.GetDirectoryName(file)}/{sprite.FileName}{sprite.FileExtension}";
            Console.Write($"Writing file: {dest}/{sprite.FileName}{sprite.FileExtension}");
            BinarySerializableIO.SaveFile(sprite, dest);
            Console.WriteLine($" ... success!");
        }
    }
}