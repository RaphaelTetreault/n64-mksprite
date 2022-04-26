using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

namespace MakeSprite
{
    internal class IndexedImage
    {
        public int PaletteSize { get; }
        public Rgba32[] Palette { get; }
        public byte[] Indexes { get; }


        public IndexedImage(Rgba32[] palette, byte[] indexes)
        {
            PaletteSize = palette.Length;
            Palette = palette;
            Indexes = indexes;
        }

        public static IndexedImage CreateIndexedImage(Image<Rgba32> image, int maxColors)
        {
            // init some settings
            var configuration = new Configuration() { };
            var quantizerOptions = new QuantizerOptions() { MaxColors = maxColors, }; // See also: dithers
            var quantizer = new WuQuantizer(quantizerOptions);
            var rgba32Quantizer = quantizer.CreatePixelSpecificQuantizer<Rgba32>(configuration);

            // build palette and indices
            var frame = image.Frames.RootFrame;
            var indexedImageFrame = rgba32Quantizer.BuildPaletteAndQuantizeFrame(frame, frame.Bounds());

            // PALETTE
            var palette = indexedImageFrame.Palette.ToArray();
            VerboseConsole.WriteLine("Palette");
            for (int i = 0; i < palette.Length; i++)
                VerboseConsole.WriteLine($"{i:x2} - {palette[i].ToHex()}");

            // INDEXES
            int capacity = image.Width * image.Height;
            var indexes = new List<byte>(capacity);
            VerboseConsole.WriteLine("Indexes");
            for (int y = 0; y < image.Height; y++)
            {
                var row = indexedImageFrame.GetWritablePixelRowSpanUnsafe(y);
                for (int x = 0; x < image.Width; x++)
                {
                    indexes.Add(row[x]);
                    VerboseConsole.Write($"{row[x]:x2} ");
                }
                VerboseConsole.WriteLine();
            }

            var indexedImage = new IndexedImage(palette, indexes.ToArray());
            return indexedImage;
        }
    }
}
