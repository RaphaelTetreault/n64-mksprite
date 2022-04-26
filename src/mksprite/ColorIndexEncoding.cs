using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeSprite
{
    internal abstract class ColorIndexEncoding : Encoding
    {
        public abstract int PaletteSize { get; }


        // Hide this, we don't use it for CI formats
        internal sealed override void WritePixel(BinaryWriter writer, Rgba32 pixel)
        {
            throw new InvalidOperationException();
        }

        public abstract override void WriteSprite(BinaryWriter writer, Image<Rgba32> image, int slicesH, int slicesV);

        public abstract void WriteIndexes(BinaryWriter writer, Image<Rgba32> image, int originX, int originY, int width, int height);

        public void WritePalette(BinaryWriter writer, Rgba32[] palette)
        {
            if (palette.Length > PaletteSize)
                throw new ArgumentOutOfRangeException();

            // Write palette
            for (int i = 0; i < palette.Length; i++)
            {
                var color = palette[i];
                FormatUtility.EncodingRGBA16.WritePixel(writer, color);
            }
            // If not full palette, write black
            int padding = PaletteSize - palette.Length;
            for (int i = 0; i < padding; i++)
                FormatUtility.EncodingRGBA16.WritePixel(writer, new Rgba32());
        }


    }
}
