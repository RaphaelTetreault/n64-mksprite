using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    internal class EncodingI4 : N64Encoding
    {
        public static EncodingI4 Encoding => new EncodingI4();

        public override int BitsPerPixel => 4;
        public override Format Format => Format.I4;


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
                        var pixel0 = image[x + 0, y];
                        var pixel1 = image[x + 1, y];

                        byte intensity0 = Rgba32ToIntensity8(pixel0);
                        byte intensity1 = Rgba32ToIntensity8(pixel1);

                        byte ia4 = (byte)(
                            ((intensity0 >> 0) & (0b11110000)) +
                            ((intensity1 >> 4) & (0b00001111)));

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
