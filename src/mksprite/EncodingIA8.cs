using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingIA8 : N64Encoding
    {
        public static EncodingIA8 Encoding => new EncodingIA8();

        public override int BitsPerPixel => 8;
        public override Format Format => Format.FMT_IA8;


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

                        // i = intensity, a = alpha
                        byte i = Rgba32ToIntensity8(pixel);
                        byte a = pixel.A;

                        byte ia8 = (byte)(
                            ((i >> 0) & 0b11110000) + 
                            ((a >> 4) & 0b00001111));

                        writer.Write(ia8);
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
