// Used libraries / NuGet packages:
//      CommandLineParser
//      ImageSharp

using CommandLine;
using Manifold;
using Manifold.IO;
using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace MakeSprite
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions);
        }

        public static void RunOptions(Options options)
        {
            //if (options.Verbose)
                options.PrintState();

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