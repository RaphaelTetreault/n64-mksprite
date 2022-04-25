using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Memory;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Extensions;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

namespace MakeSprite
{
    internal class EncodingCI8 : N64Encoding
    {
        public override int BitsPerPixel => 8;
        public override Format Format => Format.CI8;


        internal override void WritePixel(BinaryWriter writer, Rgba32 pixel)
        {
            throw new NotImplementedException();
        }

        public override void WriteSprite(BinaryWriter writer, Image<Rgba32> image, int originX, int originY, int width, int height)
        {
            Temp(writer, image, 16);

            //for (int y = originY; y < originY + height; y++)
            //{
            //    for (int x = originX; x < originX + width; x++)
            //    {
            //    }
            //}
        }

        public override Image<Rgba32> ReadSprite(Sprite sprite)
        {
            throw new NotImplementedException();
        }


        public void Temp(BinaryWriter writer, Image<Rgba32> image, int maxColors)
        {
            // init some settings
            var configuration = new Configuration() { };
            var quantizerOptions = new QuantizerOptions() { MaxColors = maxColors, }; // See also: dithers
            var quantizer = new WuQuantizer(quantizerOptions);
            var rgba32Quantizer = quantizer.CreatePixelSpecificQuantizer<Rgba32>(configuration);

            // build palette and indices
            var frame = image.Frames.RootFrame;
            var indexedImageFrame = rgba32Quantizer.BuildPaletteAndQuantizeFrame(frame, frame.Bounds());
            var palette = indexedImageFrame.Palette.ToArray();

            // debug
            Console.WriteLine("Palette");
            for (int i = 0; i < palette.Length; i++)
                Console.WriteLine($"{i:x2} - {palette[i].ToHex()}");

            Console.WriteLine("Indexes");
            for (int y = 0; y < image.Height; y++)
            {
                var row = indexedImageFrame.GetWritablePixelRowSpanUnsafe(y);
                for (int x = 0; x < image.Width; x++)
                {
                    Console.Write($"{row[x]:x2} ");
                }
                Console.WriteLine();
            }
        }
    }
}
