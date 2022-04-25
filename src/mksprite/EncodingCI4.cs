using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingCI4 : N64Encoding
    {
        public override int BitsPerPixel => 4;
        public override Format Format => Format.CI4;

        internal override void WritePixel(BinaryWriter writer, Rgba32 pixel)
        {
            throw new InvalidOperationException();
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
