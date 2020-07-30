﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AssetStudio
{
    public enum EndianType
    {
        LittleEndian,
        BigEndian
    }

    public class AssetReader : BinaryReader
    {
        public EndianType endian;
        private int offset;

        public AssetReader(Stream stream, EndianType endian = EndianType.BigEndian) : base(stream)
        {
            this.endian = endian;

            // Check for asset file type: 8 bytes leading 0s or not.
            var leadingBytes = this.ReadBytes(13);
            var target = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, (byte)'U', (byte)'n', (byte)'i', (byte)'t', (byte)'y' };
            if (leadingBytes.SequenceEqual(target))
            {
                this.offset = 8;
            }
            else
            {
                this.offset = 0;
            }
            this.Position = this.offset;
        }

        public long Position
        {
            get => BaseStream.Position - offset;
            set => BaseStream.Position = value + offset;
        }

        public override short ReadInt16()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(2);
                Array.Reverse(buff);
                return BitConverter.ToInt16(buff, 0);
            }
            return base.ReadInt16();
        }

        public override int ReadInt32()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(4);
                Array.Reverse(buff);
                return BitConverter.ToInt32(buff, 0);
            }
            return base.ReadInt32();
        }

        public override long ReadInt64()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(8);
                Array.Reverse(buff);
                return BitConverter.ToInt64(buff, 0);
            }
            return base.ReadInt64();
        }

        public override ushort ReadUInt16()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(2);
                Array.Reverse(buff);
                return BitConverter.ToUInt16(buff, 0);
            }
            return base.ReadUInt16();
        }

        public override uint ReadUInt32()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(4);
                Array.Reverse(buff);
                return BitConverter.ToUInt32(buff, 0);
            }
            return base.ReadUInt32();
        }

        public override ulong ReadUInt64()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(8);
                Array.Reverse(buff);
                return BitConverter.ToUInt64(buff, 0);
            }
            return base.ReadUInt64();
        }

        public override float ReadSingle()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(4);
                Array.Reverse(buff);
                return BitConverter.ToSingle(buff, 0);
            }
            return base.ReadSingle();
        }

        public override double ReadDouble()
        {
            if (endian == EndianType.BigEndian)
            {
                var buff = ReadBytes(8);
                Array.Reverse(buff);
                return BitConverter.ToUInt64(buff, 0);
            }
            return base.ReadDouble();
        }
    }
}
