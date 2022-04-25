using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Linq;

namespace MakeSprite
{
    internal static class FormatUtility
    {
        public static EncodingRGBA32 EncodingRGBA32 => new EncodingRGBA32();
        public static EncodingRGBA16 EncodingRGBA16 => new EncodingRGBA16();
        public static EncodingCI8 EncodingCI8 => new EncodingCI8();
        public static EncodingI8 EncodingI8 => new EncodingI8();
        public static EncodingIA8 EncodingIA8 => new EncodingIA8();
        public static EncodingCI4 EncodingCI4 => new EncodingCI4();
        public static EncodingI4 EncodingI4 => new EncodingI4();
        public static EncodingIA4 EncodingIA4 => new EncodingIA4();


        // TODO: move elsewhere
        public static N64Encoding FormatToEncoding(Format format)
        {
            switch (format)
            {
                case Format.RGBA32: return EncodingRGBA32;
                case Format.RGBA16: return EncodingRGBA16;
                case Format.CI8: return EncodingCI8;
                case Format.I8: return EncodingI8;
                case Format.IA8: return EncodingIA8;
                case Format.CI4: return EncodingCI4;
                case Format.I4: return EncodingI4;
                case Format.IA4: return EncodingIA4;

                default:
                    throw new ArgumentException($"{nameof(Format)} value '{format}' is invalid!");
            }
        }

        public static byte FormatToBitsPerPixel(Format format)
        {
            switch (format)
            {
                case Format.RGBA32:
                    return 32;

                case Format.RGBA16:
                    return 16;

                case Format.CI8:
                case Format.I8:
                case Format.IA8:
                    return 8;

                case Format.CI4:
                case Format.I4:
                case Format.IA4:
                    return 4;

                default:
                    throw new NotImplementedException();
            }
        }

        public static byte FormatToBytesPerPixel(Format format)
        {
            switch (format)
            {
                case Format.RGBA32:
                    return 4;

                case Format.RGBA16:
                    return 2;

                case Format.CI8:
                case Format.I8:
                case Format.IA8:
                    return 1;

                case Format.CI4:
                case Format.I4:
                case Format.IA4:
                    throw new NotImplementedException("I don't actually know the answer. 0? 1?");
                //return 1;

                default:
                    throw new NotImplementedException();
            }
        }

        public static double Rgba32ToIntensity(Rgba32 rgba32)
        {
            var grayscale =
                rgba32.R * 0.299 +
                rgba32.G * 0.587 +
                rgba32.B * 0.114;

            return grayscale;
        }

        public static byte Rgba32ToIntensity8(Rgba32 rgba32)
        {
            byte value = (byte)(Rgba32ToIntensity(rgba32) * byte.MaxValue);
            return value;
        }

        public static void WriteImagePalette(BinaryWriter writer, Image<Rgba32> image, int numColors)
        {
            var values = new Dictionary<Rgba32, int>();
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = image[x, y];
                    if (values.ContainsKey(pixel))
                        values[pixel]++;
                    else
                        values.Add(pixel, 1);
                }
            }

            var selected = values
                .OrderByDescending(x => x.Value)
                .Take(numColors)
                .ToList();

            for (int i = 0; i < selected.Count; i++)
            {
                var kvp = selected[i];
                EncodingRGBA16.WritePixel(writer, kvp.Key);
            }



        }

    }
}
