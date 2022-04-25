using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    public abstract class N64Encoding
    {
        public abstract int BitsPerPixel { get; }
        public abstract Format Format { get; }

        public abstract MemoryStream ConvertImage(Image<Rgba32> image);
        public abstract Image<Rgba32> ConvertSprite(Sprite sprite);


        public static int GetDataSize(Image<Rgba32> bitmap, N64Encoding encoding)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;
            var bpp = encoding.BitsPerPixel;
            var size = (int)(width * height * bpp / 8.0);
            return size;
        }
    }
}
