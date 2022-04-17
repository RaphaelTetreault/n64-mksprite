using Manifold.IO;
using System.Drawing;

namespace mksprite
{
    internal class EncodingRGBA32 : N64Encoding
    {
        public override int BitsPerPixel => 32;
        public override Format Format => Format.FMT_RGBA32;


        public override MemoryStream ConvertBitmap(Bitmap bitmap)
        {
            var capacity = GetDataSize(bitmap, this);
            var dataStream = new MemoryStream(capacity);

            using (var writer = new BinaryWriter(dataStream))
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        writer.Write(pixel.R);
                        writer.Write(pixel.G);
                        writer.Write(pixel.B);
                        writer.Write(pixel.A);
                    }
                }
            }

            return dataStream;
        }
    }
}
