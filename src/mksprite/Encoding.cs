using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    public abstract class Encoding
    {
        public abstract int BitsPerPixel { get; }
        public abstract Format Format { get; }

        public abstract Image<Rgba32> ReadSprite(BinaryReader binaryReader);

        internal abstract void WritePixel(BinaryWriter writer, Rgba32 pixel);

        public virtual void WriteSprite(BinaryWriter writer, Image<Rgba32> image, int originX, int originY, int width, int height)
        {
            for (int y = originY; y < originY + height; y++)
                for (int x = originX; x < originX + width; x++)
                    WritePixel(writer, image[x, y]);
        }

        public void WriteSprite(BinaryWriter writer, Image<Rgba32> image)
            => WriteSprite(writer, image, 0, 0, image.Width, image.Height);

        public virtual void WriteSprite(BinaryWriter writer, Image<Rgba32> image, int slicesH, int slicesV)
        {
            int width = image.Width / slicesH;
            int height = image.Height / slicesV;

            for (int y = 0; y < slicesV; y++)
            {
                int originY = y * height;

                for (int x = 0; x < slicesH; x++)
                {
                    int originX = x * width;
                    WriteSprite(writer, image, originX, originY, width, height);
                }
            }
        }

        public static int GetDataSize(Image<Rgba32> bitmap, Encoding encoding)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;
            var bpp = encoding.BitsPerPixel;
            var size = (int)(width * height * bpp / 8.0);
            return size;
        }
    }
}
