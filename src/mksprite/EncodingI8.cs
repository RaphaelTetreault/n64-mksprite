using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingI8 : Encoding
    {
        public override int BitsPerPixel => 8;
        public override Format Format => Format.I8;

        internal override void WritePixel(BinaryWriter writer, Rgba32 pixel)
        {
            byte intensity = FormatUtility.Rgba32ToIntensity8(pixel);
            writer.Write(intensity);
        }

        public override Image<Rgba32> ReadSprite(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
