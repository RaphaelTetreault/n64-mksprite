using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Memory;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Extensions;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

namespace MakeSprite
{
    internal class EncodingCI8 : ColorIndexEncoding
    {
        public override int BitsPerPixel => 8;
        public override Format Format => Format.CI8;
        public override int PaletteSize => 256;

        public override void WriteIndexes(BinaryWriter writer, Image<Rgba32> image, int originX, int originY, int width, int height)
        {
            for (int y = originY; y < originY + height; y++)
            {
                int indexY = y * image.Width;
                for (int x = originX; x < originX + width; x++)
                {
                    int index = x + indexY;
                    writer.Write(index);
                }
            }
        }

        public override void WriteSprite(BinaryWriter writer, Image<Rgba32> image, int originX, int originY, int width, int height)
        {
            var indexedImage = IndexedImage.CreateIndexedImage(image, PaletteSize);
            WritePalette(writer, indexedImage.Palette);
            WriteIndexes(writer, image, originX, originY, width, height);
        }

        public override void WriteSprite(BinaryWriter writer, Image<Rgba32> image, int slicesH, int slicesV)
        {
            var indexedImage = IndexedImage.CreateIndexedImage(image, PaletteSize);
            WritePalette(writer, indexedImage.Palette);

            int width = image.Width / slicesH;
            int height = image.Height / slicesV;

            for (int v = 0; v < slicesV; v++)
            {
                int originY = v * height;

                for (int h = 0; h < slicesH; h++)
                {
                    int originX = h * width;
                    WriteIndexes(writer, image, originX, originY, width, height);
                }
            }
        }

        public override Image<Rgba32> ReadSprite(BinaryReader reader)
        {
            throw new NotImplementedException();
        }



    }
}
