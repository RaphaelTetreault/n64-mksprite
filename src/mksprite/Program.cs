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
            if (options.Verbose)
            {
                options.PrintState();
                Console.WriteLine();
            }

            VerboseConsole.IsVerbose = options.Verbose;
            VerboseConsole.WriteLine($"Mode: {options.Mode}");


            switch (options.Mode)
            {
                case OperationMode.interactive:
                    OpModeInteractive(options);
                    throw new NotImplementedException();
                //break;
                case OperationMode.directory:
                    OpModeDirectory(options);
                    break;
                case OperationMode.file:
                    OpModeFile(options);
                    break;

                default:
                    throw new NotImplementedException("Selected mode does not exist.");
            }
        }

        public static void ShowErrors(Options options)
        {
            if (options.SlicesH != 1 || options.SlicesV != 1)
                Console.WriteLine("Slices not yet implemented.");


        }

        public static void OpModeDirectory(Options options)
        {
            var inputDoesNotExist = !Directory.Exists(options.InputPath);
            if (inputDoesNotExist)
            {
                VerboseConsole.WriteLine($"Path provided is not a valid directory!");
                VerboseConsole.WriteLine($"\t{nameof(options.InputPath)}:{options.InputPath}");
                return;
            }

            var definedOutputPath = !string.IsNullOrEmpty(options.OutputPath);
            var outputDoesNotExist = !Directory.Exists(options.OutputPath);
            if (definedOutputPath && outputDoesNotExist)
            {
                VerboseConsole.WriteLine($"Path provided is not a valid directory!");
                VerboseConsole.WriteLine($"\t{nameof(options.OutputPath)}:{options.OutputPath}");
                return;
            }

            var files = Directory.GetFiles(options.InputPath, options.SearchPattern, options.SearchOption);
            if (files.IsNullOrEmpty())
            {
                VerboseConsole.WriteLine($"Pattern found no matches in path provided!");
                VerboseConsole.WriteLine($"\t{nameof(options.InputPath)}:{options.InputPath}");
                VerboseConsole.WriteLine($"\t{nameof(options.SearchPattern)}:{options.SearchPattern}");
                VerboseConsole.WriteLine($"\t{nameof(options.SearchSubdirectories)}:{options.SearchSubdirectories}");
            }

            VerboseConsole.WriteLine($"Found {files.Length} files.");
            foreach (var file in files)
            {
                var sprite = LoadImageAsSprite(file, options);
                SaveFile(sprite, file, options);
            }
        }

        public static void OpModeFile(Options options)
        {
            var inputDoesNotExist = !File.Exists(options.InputPath);
            if (inputDoesNotExist)
            {
                VerboseConsole.WriteLine($"Path provided is not a valid file!");
                VerboseConsole.WriteLine($"\t{nameof(options.InputPath)}:{options.InputPath}");
                return;
            }

            var sprite = LoadImageAsSprite(options.InputPath, options);
            SaveFile(sprite, options.InputPath, options);
        }

        public static void OpModeInteractive(Options options)
        {
            throw new NotImplementedException();
        }

        // TODO: implement resampler selection
        public static void ResizeImage(Image image, Options options)
        {
            bool doResizeW = options.ResizeW != null;
            bool doResizeH = options.ResizeH != null;
            //if (doResizeW || doResizeH)
            //{
#pragma warning disable CS8629 // Nullable value type may be null.
                int w = doResizeW ? (int)options.ResizeW : image.Width;
                int h = doResizeH ? (int)options.ResizeH : image.Height;
#pragma warning restore CS8629 // Nullable value type may be null.
                image.Mutate(ipc => ipc.Resize(w, h));
            //}
        }

        public static Sprite LoadImageAsSprite(string filePath, Options options)
        {
            // Load image file
            var image = Image.Load(filePath) as Image<Rgba32>;

            // Make sure we got it
            if (image == null)
                throw new FileLoadException("Failed to load file.", filePath);
            VerboseConsole.WriteLine($"Read file: {filePath}");

            // If the user wants to resize the iamge, do that now
            if (options.UserWantsResize)
                ResizeImage(image, options);

            // Create sprite using image and option parameters
            var sprite = new Sprite()
            {
                FileName = Path.GetFileNameWithoutExtension(filePath),
                Width = (ushort)image.Width,
                Height = (ushort)image.Height,
                Format = options.Format,
                BitDepth = N64Encoding.FormatToBitsPerPixel(options.Format),
                SlicesH = checked((byte)options.SlicesH),
                SlicesV = checked((byte)options.SlicesV),
            };
            sprite.SetImage(image, options.Format);

            return sprite;
        }


        public static string GetFileOutputPath(string inputFilePath, IFileType outputFile, Options options)
        {
            bool hasOutputPath = !string.IsNullOrEmpty(options.OutputPath);

            string directory = !hasOutputPath
                ? Path.GetDirectoryName(inputFilePath)
                : options.OutputPath;

            if (hasOutputPath)
            {
                string beyondRootPath = inputFilePath.Replace(options.InputPath, "");
                string subDirectories = Path.GetDirectoryName(beyondRootPath);

                var hasSubdirectories = !string.IsNullOrEmpty(subDirectories);
                if (hasSubdirectories)
                    directory = Path.Combine(directory, subDirectories);

                var directoryPathDoesNotExists = !Directory.Exists(directory);
                if (directoryPathDoesNotExists)
                    Directory.CreateDirectory(directory);
            }

            string outputPath = $"{directory}/{outputFile.FileName}{outputFile.FileExtension}";
            return outputPath;
        }



        public static void SaveFile<TBinarySerializable>(TBinarySerializable value, string inputFilePath, Options options)
            where TBinarySerializable : IBinaryFileType, IBinarySerializable
        {
            var outputPath = GetFileOutputPath(inputFilePath, value, options);
            Console.Write($"Writing file: {outputPath}");
            BinarySerializableIO.SaveFile(value, outputPath);
            Console.WriteLine($" ... success!");
        }

    }
}