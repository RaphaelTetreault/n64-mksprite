using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingI8 : N64Encoding
    {
        public override int BitsPerPixel => 8;
        public override Format Format => Format.I8;


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
                        byte intensity = FormatUtility.Rgba32ToIntensity8(pixel);
                        writer.Write(intensity);
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
