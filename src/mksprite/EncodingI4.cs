using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingI4 : Encoding
    {
        public override int BitsPerPixel => 4;
        public override Format Format => Format.I4;


        internal override void WritePixel(BinaryWriter writer, Rgba32 pixel)
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

                    byte intensity0 = FormatUtility.Rgba32ToIntensity8(pixel0);
                    byte intensity1 = FormatUtility.Rgba32ToIntensity8(pixel1);

                    byte ia4 = (byte)(
                        ((intensity0 >> 0) & (0b11110000)) +
                        ((intensity1 >> 4) & (0b00001111)));

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
