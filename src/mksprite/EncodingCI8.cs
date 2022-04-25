using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

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
            for (int y = originY; y < originY + height; y++)
            {
                for (int x = originX; x < originX + width; x++)
                {
                    throw new NotImplementedException();
                }
            }
        }

        public override Image<Rgba32> ReadSprite(Sprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
