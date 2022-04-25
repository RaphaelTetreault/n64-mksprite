using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingRGBA16 : N64Encoding
    {
        public override int BitsPerPixel => 16;
        public override Format Format => Format.RGBA16;


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
                        var pixel = image[x, y];
                        ushort rgba16 = (ushort)(
                            ((pixel.R << 8) & 0b11111000_00000000) +
                            ((pixel.G << 3) & 0b00000111_11000000) +
                            ((pixel.B >> 2) & 0b00000000_00111110) +
                            ((pixel.A >> 7) & 0b00000000_00000001));
                        writer.Write(rgba16);
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