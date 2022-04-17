using System.Drawing;

namespace MakeSprite
{
    public abstract class N64Encoding
    {
        public abstract int BitsPerPixel { get; }
        public abstract Format Format { get; }

        public abstract MemoryStream ConvertBitmap(Bitmap bitmap);

        public static int GetDataSize(Bitmap bitmap, N64Encoding encoding)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;
            var bpp = encoding.BitsPerPixel;
            var size = (int)(width * height * bpp / 32.0);
            return size;
        }
    }
}
