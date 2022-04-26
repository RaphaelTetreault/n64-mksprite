using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingCI4 : ColorIndexEncoding
    {
        public override int BitsPerPixel => 4;
        public override Format Format => Format.CI4;
        public override int PaletteSize => 16;


        public override void WriteIndexes(BinaryWriter writer, Image<Rgba32> image, int originX, int originY, int width, int height)
        {
            for (int y = originY; y < originY + height; y++)
            {
                int indexY = y * image.Width;

                // write 2 indexes at a time
                for (int x = originX; x < originX + width; x += 2)
                {
                    int index0 = x + 0 + indexY;
                    int index1 = x + 1 + indexY;
                    byte indexes01 = (byte)(
                        ((index0 >> 4) & 0b11110000) + 
                        ((index1 >> 0) & 0b00001111));
                    writer.Write(indexes01);
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
