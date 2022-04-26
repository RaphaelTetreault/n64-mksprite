using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingI8 : Encoding
    {
        public override int BitsPerPixel => 8;
        public override Format Format => Format.I8;

        internal override void WriteColor(BinaryWriter writer, Rgba32 color)
        {
            byte intensity = FormatUtility.Rgba32ToIntensity8(color);
            writer.Write(intensity);
        }

        public override Image<Rgba32> ReadSprite(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
