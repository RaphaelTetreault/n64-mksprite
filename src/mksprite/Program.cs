using CommandLine;
using Manifold;
using Manifold.IO;
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

            ShowWarnings(options);

            switch (options.Mode)
            {
                case OperationMode.interactive:
                    OpModeInteractive(options);
                    break;
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

        public static void ShowWarnings(Options options)
        {
            if (options.SlicesH != 1 || options.SlicesV != 1)
                Console.WriteLine("Slices not yet implemented.");
        }

        public static void OpModeDirectory(Options options)
        {
            var inputDoesNotExist = !Directory.Exists(options.InputPath);
            if (inputDoesNotExist)
            {
                Console.WriteLine($"Path provided is not a valid directory!");
                VerboseConsole.WriteLine($"\t{nameof(options.InputPath)}:{options.InputPath}");
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

            Console.WriteLine($"Found {files.Length} files.");
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
                Console.WriteLine($"Path provided is not a valid file!");
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



        public static string GetFileOutputPath(string inputFilePath, IFileType outputFile, Options options)
        {
            string inputPath = Path.GetFullPath(inputFilePath);
            string outputPath = Path.GetFullPath(options.OutputPath);
            bool hasOutputPath = !string.IsNullOrEmpty(options.OutputPath);

            string directory = hasOutputPath
                ? Path.GetFullPath(outputPath)      // Get specified output directory
                : Path.GetDirectoryName(inputPath); // Get file's directory

            bool isDirectoryMode = options.Mode == OperationMode.directory;
            if (hasOutputPath && isDirectoryMode)
            {
                // Make sure we are not using relative file paths here
                var inputFile = Path.GetFullPath(inputFilePath);
                var inputDirectory = Path.GetFullPath(options.InputPath);
                var outputDirectory = Path.GetFullPath(options.OutputPath);

                // GOAL remove any leading input path specified by the search method that found
                //      this file in 'directory' mode.
                // EX:
                //      input   = "a/b/"
                //      output  = "x/y/"
                //      pattern = "*.png"
                //      (found) = "a/b/c/spr.png"
                //
                // The Replace() call trims the file path.  ("c/spr.png").
                // Code then grabs the directories          ("c/"),
                // and appends it to the output directory.  ("x/y/c/").
                // It then grabs the file name              ("spr.png")
                // and appends it, and get the output.      ("x/y/c/spr.png")
                string beyondRootPath = inputFile.Replace(inputDirectory, "");
                string subDirectories = Path.GetDirectoryName(beyondRootPath);
                // Set destination folder as root
                // Simply add strings due to strange Path.Combine behaviour
                // https://stackoverflow.com/questions/53102/why-does-path-combine-not-properly-concatenate-filenames-that-start-with-path-di
                directory = outputDirectory + subDirectories;

                // Ensure we have the directory path
                Directory.CreateDirectory(directory);
            }

            string destination = $"{directory}/{outputFile.FileName}{outputFile.FileExtension}";
            return destination;
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

            var fileName = options.RemoveAllExtensions
                ? Path.GetFileName(filePath).Split('.')[0]
                : Path.GetFileNameWithoutExtension(filePath);

            // Create sprite using image and option parameters
            var sprite = new Sprite()
            {
                FileName = fileName,
                Width = (ushort)image.Width,
                Height = (ushort)image.Height,
                Format = options.Format,
                BitDepth = FormatUtility.FormatToBitsPerPixel(options.Format),
                SlicesH = checked((byte)options.SlicesH),
                SlicesV = checked((byte)options.SlicesV),
                Image = image,
            };

            return sprite;
        }

        public static void ResizeImage(Image image, Options options)
        {
            var resampler = options.IResampler;
            int w = options.ResizeW != null ? (int)options.ResizeW : image.Width;
            int h = options.ResizeH != null ? (int)options.ResizeH : image.Height;
            image.Mutate(ipc => ipc.Resize(w, h, resampler));
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