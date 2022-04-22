//using System.Drawing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    public abstract class N64Encoding
    {
        public abstract int BitsPerPixel { get; }
        public abstract Format Format { get; }

        public abstract MemoryStream ConvertImage(Image<Rgba32> bitmap);
        public abstract Image<Rgba32> ConvertSprite(Sprite sprite);

        public static int GetDataSize(Image<Rgba32> bitmap, N64Encoding encoding)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;
            var bpp = encoding.BitsPerPixel;
            var size = (int)(width * height * bpp / 8.0);
            return size;
        }

        // TODO: move elsewhere
        public static N64Encoding FormatToEncoding(Format format)
        {
            switch (format)
            {
                case Format.FMT_RGBA32: return EncodingRGBA32.Encoding;

                default:
                    throw new NotImplementedException();
            }
        }
        public static byte FormatToBitsPerPixel(Format format)
        {
            switch (format)
            {
                case Format.FMT_RGBA32:
                    return 32;

                case Format.FMT_RGBA16:
                    return 16;

                case Format.FMT_CI8:
                case Format.FMT_I8:
                case Format.FMT_IA8:
                    return 8;

                case Format.FMT_CI4:
                case Format.FMT_I4:
                case Format.FMT_IA4:
                    return 4;

                default:
                    throw new NotImplementedException();
            }
        }

        public static byte FormatToBytesPerPixel(Format format)
        {
            switch (format)
            {
                case Format.FMT_RGBA32:
                    return 4;

                case Format.FMT_RGBA16:
                    return 2;

                case Format.FMT_CI8:
                case Format.FMT_I8:
                case Format.FMT_IA8:
                    return 1;

                case Format.FMT_CI4:
                case Format.FMT_I4:
                case Format.FMT_IA4:
                    throw new NotImplementedException("I don't actually know the answer. 0? 1?");
                    //return 1;

                default:
                    throw new NotImplementedException();
            }
        }

    }
}
