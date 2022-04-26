using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingIA4 : Encoding
    {
        public override int BitsPerPixel => 4;
        public override Format Format => Format.IA4;

        internal override void WriteColor(BinaryWriter writer, Rgba32 color)
        {
            throw new InvalidOperationException();
        }

        public override void WriteSprite(BinaryWriter writer, Image<Rgba32> image, int originX, int originY, int width, int height)
        {
            for (int y = originY; y < originY + height; y++)
            {
                // process 2 pixels at a time
                for (int x = originX; x < originX + width; x += 2)
                {
                    var pixel0 = image[x + 0, y];
                    var pixel1 = image[x + 1, y];

                    // i = intensity, a = alpha
                    byte i0 = FormatUtility.Rgba32ToIntensity8(pixel0);
                    byte i1 = FormatUtility.Rgba32ToIntensity8(pixel1);
                    byte a0 = pixel0.A;
                    byte a1 = pixel1.A;

                    byte ia4 = (byte)(
                        ((i0 >> 0) & (0b11100000)) +
                        ((a0 >> 3) & (0b00010000)) +
                        ((i1 >> 4) & (0b00001110)) +
                        ((a1 >> 7) & (0b00000001)));

                    writer.Write(ia4);
                }
            }
        }

        public override Image<Rgba32> ReadSprite(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
