using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingRGBA16 : Encoding
    {
        public override int BitsPerPixel => 16;
        public override Format Format => Format.RGBA16;


        internal override void WriteColor(BinaryWriter writer, Rgba32 color)
        {
            ushort rgba16 = (ushort)(
                ((color.R << 8) & 0b11111000_00000000) +
                ((color.G << 3) & 0b00000111_11000000) +
                ((color.B >> 2) & 0b00000000_00111110) +
                ((color.A >> 7) & 0b00000000_00000001));

            writer.Write(rgba16);
        }

        public override Image<Rgba32> ReadSprite(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}