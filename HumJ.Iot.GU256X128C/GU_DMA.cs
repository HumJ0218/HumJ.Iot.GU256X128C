namespace HumJ.Iot.GU256X128C
{
    public static class GU_DMA
    {
        /// <summary>
        /// Write bit image data to the specified address.
        /// </summary>
        /// <param name="dad">Display address</param>
        /// <param name="a">Bit image write address</param>
        /// <param name="dn">Bit image data</param>
        public static byte[] BitImageWrite(byte dad, int a, Span<byte> dn)
        {
            var s = dn.Length;

            var aL = (byte)(a >> 0);
            var aH = (byte)(a >> 8);
            var sL = (byte)(s >> 0);
            var sH = (byte)(s >> 8);

            var bytes = new byte[8 + s];
            bytes[0] = 0x02;
            bytes[1] = 0x44;
            bytes[2] = dad;
            bytes[3] = 0x46;
            bytes[4] = aL;
            bytes[5] = aH;
            bytes[6] = sL;
            bytes[7] = sH;

            dn.CopyTo(bytes.AsSpan(8));
            return bytes;
        }

        /// <summary>
        /// Write bit image data to the specified area.
        /// </summary>
        /// <param name="dad">Display address</param>
        /// <param name="a">Bit image write start address</param>
        /// <param name="sX">Bit image write size X</param>
        /// <param name="sY">Bit image write size Y</param>
        /// <param name="dn">Bit image data</param>
        public static byte[] BoxAreaBitImageWrite(byte dad, int a, int sX, int sY, byte[] dn)
        {
            var aL = (byte)(a >> 0);
            var aH = (byte)(a >> 8);
            var sXL = (byte)(sX >> 0);
            var sXH = (byte)(sX >> 8);
            var sYL = (byte)(sY >> 0);
            var sYH = (byte)(sY >> 8);

            var bytes = new byte[] { 0x02, 0x44, dad, 0x42, aL, aH, sXL, sXH, sYL, sYH }.Concat(dn).ToArray();
            return bytes;
        }

        /// <summary>
        /// Set the Display start address.
        /// </summary>
        /// <param name="dad">Display address</param>
        /// <param name="a">Display start address</param>
        public static byte[] DisplayStartAddress(byte dad, int a)
        {
            var aL = (byte)(a >> 0);
            var aH = (byte)(a >> 8);

            var bytes = new byte[] { 0x02, 0x44, dad, 0x53, aL, aH };
            return bytes;
        }

        /// <summary>
        /// Synchronizes the next command with internal display refresh cycle.
        /// </summary>
        /// <param name="dad">Display address</param>
        public static byte[] DisplaySynchronous(byte dad)
        {
            var bytes = new byte[] { 0x02, 0x44, dad, 0x57, 0x01 };
            return bytes;
        }

        /// <summary>
        /// Set brightness level.
        /// </summary>
        /// <param name="dad">Display address</param>
        /// <param name="n">Brightness level setting</param>
        public static byte[] BrightnessLevel(byte dad, BrightnessLevel n)
        {
            var bytes = new byte[] { 0x02, 0x44, dad, 0x58, (byte)n };
            return bytes;
        }
    }
}