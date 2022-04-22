using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingCI4 : N64Encoding
    {
        public static EncodingCI4 Encoding => new EncodingCI4();

        public override int BitsPerPixel => 4;
        public override Format Format => Format.CI4;


        public override MemoryStream ConvertImage(Image<Rgba32> image)
        {
            var capacity = GetDataSize(image, this);
            var dataStream = new MemoryStream(capacity);

            using (var writer = new BinaryWriter(dataStream))
            {
                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        throw new NotImplementedException();
                        //var pixel = image[x, y];
                        //ushort rgba16 = (ushort)(
                        //    (pixel.R / (1 << 4)) << 12 +
                        //    (pixel.G / (1 << 4)) << 08 +
                        //    (pixel.B / (1 << 4)) << 04 +
                        //    (pixel.A / (1 << 4)) << 00);
                        //writer.Write(rgba16);
                    }
                }
            }

            return dataStream;
        }

        public override Image<Rgba32> ConvertSprite(Sprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
