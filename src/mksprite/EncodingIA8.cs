using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingIA8 : N64Encoding
    {

        public override int BitsPerPixel => 8;
        public override Format Format => Format.IA8;

        internal override void WritePixel(BinaryWriter writer, Rgba32 pixel)
        {
            // i = intensity, a = alpha
            byte i = FormatUtility.Rgba32ToIntensity8(pixel);
            byte a = pixel.A;

            byte ia8 = (byte)(
                ((i >> 0) & 0b11110000) +
                ((a >> 4) & 0b00001111));

            writer.Write(ia8);
        }

        public override Image<Rgba32> ReadSprite(Sprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
