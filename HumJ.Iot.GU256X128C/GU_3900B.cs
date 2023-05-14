using System.IO.Ports;

namespace HumJ.Iot.GU256X128C
{
    public class GU_3900B : GU_3900
    {
        public GU_3900B(SerialPort port) : base(port)
        {
        }

        public virtual void LineClear()
        {
            WriteBytes(new byte[] { 0x18 });
        }

        public virtual void LineEndClear()
        {
            WriteBytes(new byte[] { 0x19 });
        }

        public virtual void HorizontalScrollModeScrollOn()
        {
            WriteBytes(new byte[] { 0x1F, 0x05 });
        }

        public virtual void FontWidth(FontWidth m)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x04, (byte)m });
        }

        public virtual void FRomExtendedFont(byte n)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x05, n });
        }

        public virtual void DisplayPowerAutoOffTime(byte t)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x40, 0x11, t });
        }

        public virtual void DotUnitDownloadedBitImageDisplay(int xP, int yP, byte m, int a, int yS, int xO, int yO, int x, int y)
        {
            var xPL = (byte)(xP >> 0);
            var xPH = (byte)(xP >> 8);
            var yPL = (byte)(yP >> 0);
            var yPH = (byte)(yP >> 8);
            var aL = (byte)(a >> 0);
            var aH = (byte)(a >> 8);
            var aE = (byte)(a >> 16);
            var ySL = (byte)(yS >> 0);
            var ySH = (byte)(yS >> 8);
            var xOL = (byte)(xO >> 0);
            var xOH = (byte)(xO >> 8);
            var yOL = (byte)(yO >> 0);
            var yOH = (byte)(yO >> 8);
            var xL = (byte)(x >> 0);
            var xH = (byte)(x >> 8);
            var yL = (byte)(y >> 0);
            var yH = (byte)(y >> 8);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x64, 0x20, xPL, xPH, yPL, yPH, m, aL, aH, aE, ySL, ySH, xOL, xOH, yOL, yOH, xL, xH, yL, yH, 0x01 });
        }

        public virtual void DotUnitRealtimeBitImageDisplay(int xP, int yP, int x, int y, byte[] dn)
        {
            var xPL = (byte)(xP >> 0);
            var xPH = (byte)(xP >> 8);
            var yPL = (byte)(yP >> 0);
            var yPH = (byte)(yP >> 8);
            var xL = (byte)(x >> 0);
            var xH = (byte)(x >> 8);
            var yL = (byte)(y >> 0);
            var yH = (byte)(y >> 8);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x64, 0x21, xPL, xPH, yPL, yPH, xL, xH, yL, yH, 01 }.Concat(dn).ToArray());
        }

        public virtual void DotUnitCharacterDisplay(int xP, int yP, int x, int y, byte m, byte bLen, byte[] dn)
        {
            var xPL = (byte)(xP >> 0);
            var xPH = (byte)(xP >> 8);
            var yPL = (byte)(yP >> 0);
            var yPH = (byte)(yP >> 8);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x64, 0x30, xPL, xPH, yPL, yPH, m, bLen }.Concat(dn).ToArray());
        }

        public virtual void Font32x32DownloadedCharacterDefinition(byte c1, byte c2, byte[] xn)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x14, c1, c2 }.Concat(xn).ToArray());
        }

        public virtual void Font32x32DownloadedCharacterDelete(byte c1, byte c2)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x15, c1, c2 });
        }

        public virtual void FRoomExtensionFontDefintion(byte a, byte b, byte[] p)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x15, a, b }.Concat(p).ToArray());
        }

        public virtual void MacroEndCondition(byte a, byte b, byte c)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x69, 0x20, a, b, c });
        }

        public virtual void GeneralpurposeMemoryStore(int s, byte m1, int a1, byte[] dn)
        {
            var sL = (byte)(s >> 0);
            var sH = (byte)(s >> 8);
            var sE = (byte)(s >> 16);
            var a1L = (byte)(a1 >> 0);
            var a1H = (byte)(a1 >> 8);
            var a1E = (byte)(a1 >> 16);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x18, sL, sH, sE, m1, a1L, a1H, a1E }.Concat(dn).ToArray());
        }

        public virtual void GeneralpurposeMemoryTransfer(int s, byte m1, int a1, byte m2, int a2)
        {
            var sL = (byte)(s >> 0);
            var sH = (byte)(s >> 8);
            var sE = (byte)(s >> 16);
            var a1L = (byte)(a1 >> 0);
            var a1H = (byte)(a1 >> 8);
            var a1E = (byte)(a1 >> 16);
            var a2L = (byte)(a2 >> 0);
            var a2H = (byte)(a2 >> 8);
            var a2E = (byte)(a2 >> 16);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x19, sL, sH, sE, m1, a1L, a1H, a1E, m2, a2L, a2H, a2E });
        }

        public virtual void GeneralpurposeMemorySend(int s, byte m1, int a1)
        {
            var sL = (byte)(s >> 0);
            var sH = (byte)(s >> 8);
            var sE = (byte)(s >> 16);
            var a1L = (byte)(a1 >> 0);
            var a1H = (byte)(a1 >> 8);
            var a1E = (byte)(a1 >> 16);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x28, sL, sH, sE, m1, a1L, a1H, a1E });
        }

        public virtual void Rs232SerialSettings(Rs232SerialSettingsBaudrate a, Rs232SerialSettingsParity b)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x69, 0x10, (byte)a, (byte)b });
        }
    }
    public enum Rs232SerialSettingsBaudrate : byte
    {
        // Baudrate19200 = 0,
        Baudrate4800 = 1,
        Baudrate9600 = 2,
        Baudrate19200 = 3,
        Baudrate38400 = 4,
        Baudrate57600 = 5,
        Baudrate115200 = 6,
    }
    public enum Rs232SerialSettingsParity : byte
    {
        None = 0,
        Even = 1,
        Odd = 2,
    }
}