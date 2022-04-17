using Manifold.IO;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private byte[] data;

        public ushort Width { get => width; set => width = value; }
        public ushort Height { get => height; set => height = value; }
        public byte BitDepth { get => bitDepth; set => bitDepth = value; }
        public Format Format { get => format; set => format = value; }
        public byte SlicesH { get => slicesH; set => slicesH = value; }
        public byte SlicesV { get => slicesV; set => slicesV = value; }

        public int Size => (int)(width * height * bitDepth / 32.0);

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
            reader.Read(ref data, Size);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(width);
            writer.Write(height);
            writer.Write(bitDepth);
            writer.Write(format);
            writer.Write(slicesH);
            writer.Write(slicesV);
            writer.Write(data);
        }

        public void SetBitmap(Bitmap bitmap, N64Encoding encoding)
        {
            var stream = encoding.ConvertBitmap(bitmap);
            data = stream.ToArray();
        }

        public Bitmap GetBitmap()
        {
            throw new NotImplementedException();
        }

    }
}
