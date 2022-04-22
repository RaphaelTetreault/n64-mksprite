using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingIA4 : N64Encoding
    {
        public static EncodingIA4 Encoding => new EncodingIA4();

        public override int BitsPerPixel => 4;
        public override Format Format => Format.IA4;


        public override MemoryStream ConvertImage(Image<Rgba32> image)
        {
            var capacity = GetDataSize(image, this);
            var dataStream = new MemoryStream(capacity);

            using (var writer = new BinaryWriter(dataStream))
            {
                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x += 2)
                    {
                        // process 2 pixels at a time
                        var pixel0 = image[x+0, y];
                        var pixel1 = image[x+1, y];

                        // i = intensity, a = alpha
                        byte i0 = Rgba32ToIntensity8(pixel0);
                        byte i1 = Rgba32ToIntensity8(pixel1);
                        byte a0 = pixel0.A;
                        byte a1 = pixel1.A;

                        byte ia4 = (byte)(
                            ((i0 >> 0) & (0b11100000)) +
                            ((a0 >> 3) & (0b00010000)) +
                            ((i1 >> 4) & (0b00001110)) +
                            ((a1 >> 7) & (0b00000001)));

                        writer.Write(ia4);
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
