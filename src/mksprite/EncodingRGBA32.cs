using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingRGBA32 : Encoding
    {
        public override int BitsPerPixel => 32;
        public override Format Format => Format.RGBA32;


        internal override void WritePixel(BinaryWriter writer, Rgba32 pixel)
        {
            writer.Write(pixel.R);
            writer.Write(pixel.G);
            writer.Write(pixel.B);
            writer.Write(pixel.A);
        }

        public override Image<Rgba32> ReadSprite(BinaryReader reader)
        {
            throw new NotImplementedException();

            //for (int y = 0; y < image.Height; y++)
            //{
            //    for (int x = 0; x < image.Width; x++)
            //    {
            //        var r = reader.ReadByte();
            //        var g = reader.ReadByte();
            //        var b = reader.ReadByte();
            //        var a = reader.ReadByte();
            //        var pixel = new Rgba32(r, g, b, a);
            //        image[x, y] = pixel;
            //    }
            //}
        }
    }
}
