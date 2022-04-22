//using System.Drawing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingRGBA32 : N64Encoding
    {
        public static EncodingRGBA32 Encoding => new EncodingRGBA32();

        public override int BitsPerPixel => 32;
        public override Format Format => Format.FMT_RGBA32;


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
                        writer.Write(pixel.R);
                        writer.Write(pixel.G);
                        writer.Write(pixel.B);
                        writer.Write(pixel.A);
                    }
                }
            }

            return dataStream;
        }

        public override Image<Rgba32> ConvertSprite(Sprite sprite)
        {
            var image = new Image<Rgba32>(sprite.Width, sprite.Height);
            var dataStream = new MemoryStream(sprite.Data);

            using (var reader = new BinaryReader(dataStream))
            {
                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        var r = reader.ReadByte();
                        var g = reader.ReadByte();
                        var b = reader.ReadByte();
                        var a = reader.ReadByte();
                        var pixel = new Rgba32(r, g, b, a);
                        image[x, y] = pixel;
                    }
                }
            }

            return image;
        }

    }
}
