using Manifold.IO;
using System;
//using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    public class Sprite :
        IBinarySerializable,
        IBinaryFileType
    {
        private ushort width;
        private ushort height;
        private byte bitDepth;
        private Format format;
        private byte slicesH;
        private byte slicesV;
        private Image<Rgba32> image;

        public ushort Width { get => width; set => width = value; }
        public ushort Height { get => height; set => height = value; }
        public byte BitDepth { get => bitDepth; set => bitDepth = value; }
        public Format Format { get => format; set => format = value; }
        public byte SlicesH { get => slicesH; set => slicesH = value; }
        public byte SlicesV { get => slicesV; set => slicesV = value; }
        public int Size => (int)(width * height * bitDepth / 8.0);
        public Image<Rgba32> Image { get => image; set => image = value; }

        public Endianness Endianness => Endianness.BigEndian;
        public string FileExtension => ".sprite";
        public string FileName { get; set; } = string.Empty;


        public void Deserialize(EndianBinaryReader reader)
        {
            reader.Read(ref width);
            reader.Read(ref height);
            reader.Read(ref bitDepth);
            reader.Read(ref format);
            reader.Read(ref slicesH);
            reader.Read(ref slicesV);
            
            var encoding = FormatUtility.FormatToEncoding(Format);
            image = encoding.ReadSprite(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(width);
            writer.Write(height);
            writer.Write(bitDepth);
            writer.Write(format);
            writer.Write(slicesH);
            writer.Write(slicesV);

            var encoding = FormatUtility.FormatToEncoding(Format);
            encoding.WriteSprite(writer, image, SlicesH, slicesV);
        }

    }
}
