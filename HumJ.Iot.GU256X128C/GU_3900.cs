using System.Collections.Generic;
using System.IO.Ports;
using System.Reflection.Metadata;
using System.Text;

namespace HumJ.Iot.GU256X128C
{
    public partial class GU_3900
    {
        public const int DefaultBaudRate = 38400;
        public const int DefaultDataBits = 8;
        public const Parity DefaultParity = Parity.None;
        public const StopBits DefaultStopBits = StopBits.One;
        public const bool DefaultDtrEnable = true;

        internal readonly SerialPort serialPort;

        public GU_3900(SerialPort port)
        {
            serialPort = port;
        }

        protected void WriteString(string s, Encoding? encoding = null)
        {
            if (encoding == null)
            {
                serialPort.Write(s);
            }
            else
            {
                var bytes = encoding.GetBytes(s);
                WriteBytes(bytes);
            }
        }

        protected void WriteBytes(byte[] bytes)
        {
            serialPort.Write(bytes, 0, bytes.Length);
        }

        protected void WriteBytes(byte[] bytes, byte[] more)
        {
            var buffer = bytes.Concat(more).ToArray();
            serialPort.Write(buffer, 0, buffer.Length);
        }

        public virtual void CharacterDisplay(string s, Encoding? encoding = null)
        {
            WriteString(s, encoding);
        }

        public virtual void BackSpace()
        {
            WriteBytes(new byte[] { 0x08 });
        }

        public virtual void HorizontaTab()
        {
            WriteBytes(new byte[] { 0x09 });
        }

        public virtual void LineFeed()
        {
            WriteBytes(new byte[] { 0x0A });
        }

        public virtual void HomePosition()
        {
            WriteBytes(new byte[] { 0x0B });
        }

        public virtual void CarriageReturn()
        {
            WriteBytes(new byte[] { 0x0D });
        }

        public virtual void DisplayClear()
        {
            WriteBytes(new byte[] { 0x0C });
        }

        public virtual void BrightnessLevelSetting(BrightnessLevel n)
        {
            WriteBytes(new byte[] { 0x1F, 0x58, (byte)n });
        }

        public virtual void InitializeDisplay()
        {
            WriteBytes(new byte[] { 0x1B, 0x40 });
        }

        public virtual void CursorSet(int x, int y)
        {
            var xL = (byte)(x >> 0);
            var xH = (byte)(x >> 8);
            var yL = (byte)(y >> 0);
            var yH = (byte)(y >> 8);

            WriteBytes(new byte[] { 0x1F, 0x24, xL, xH, yL, yH });
        }

        public virtual void CursorDisplay(CursorDisplay n)
        {
            WriteBytes(new byte[] { 0x1F, 0x43,(byte) n });
        }

        public virtual void WriteScreenModeSelect(WriteScreenMode a)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x77, 0x10, (byte)a });
        }

        public virtual void InternationalFontSet(FontSet n)
        {
            WriteBytes(new byte[] { 0x1B, 0x52, (byte)n });
        }

        public virtual void CharacterTableType(FontCodeType n)
        {
            WriteBytes(new byte[] { 0x1B, 0x74, (byte)n });
        }

        public virtual void OverwriteMode()
        {
            WriteBytes(new byte[] { 0x1F, 0x01 });
        }

        public virtual void VerticalScrollMode()
        {
            WriteBytes(new byte[] { 0x1F, 0x02 });
        }

        public virtual void HorizontalScrollMode()
        {
            WriteBytes(new byte[] { 0x1F, 0x03 });
        }

        public virtual void HorizontalScrollSpeed(byte n)
        {
            WriteBytes(new byte[] { 0x1F, 0x73, n });
        }

        public virtual void FontSizeSelect(FontSize m)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x01, (byte)m });
        }

        public virtual void TwoByteCharacter(bool enabled)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x02, (byte)(enabled?1:0) });
        }

        public virtual void TwoByteCharacterType(CodeType m)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x03, (byte)m });
        }

        public virtual void FontMagnification(byte x, byte y)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x40, x, y });
        }

        public virtual void BoldCharacter(byte b)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x41, b });
        }

        public virtual void Wait(byte t)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x01, t });
        }

        public virtual void ShortWait(byte t)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x02, t });
        }

        public virtual void ScrollDisplayAction(int w, int c, byte s)
        {
            var wL = (byte)(w >> 0);
            var wH = (byte)(w >> 8);
            var cL = (byte)(c >> 0);
            var cH = (byte)(c >> 8);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x10, wL, wH, cL, cH, s });
        }

        public virtual void Blink(byte p, byte t1, byte t2, byte c)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x11, p, t1, t2, c });
        }

        public virtual void CurtainDisplayAction(byte v, byte s, byte p)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x12, v, s, p });
        }

        public virtual void SpringDisplayAction(byte v, byte s, int p)
        {
            var pL = (byte)(p >> 0);
            var pH = (byte)(p >> 8);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x13, v, s, pL, pH });
        }

        public virtual void RandomDisplayAction(byte s, int p)
        {
            var pL = (byte)(p >> 0);
            var pH = (byte)(p >> 8);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x14, s, pL, pH });
        }

        public virtual void DisplayPowerOnOff(DisplayPowerOnOff p)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x61, 0x40, (byte)p });
        }

        public virtual void DotDrawing(byte pen, int x, int y)
        {
            var xL = (byte)(x >> 0);
            var xH = (byte)(x >> 8);
            var yL = (byte)(y >> 0);
            var yH = (byte)(y >> 8);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x64, 0x10, pen, xL, xH, yL, yH });
        }

        public virtual void LineBoxPatternDrawing(byte mode, byte pen, int x1, int y1, int x2, int y2)
        {
            var x1L = (byte)(x1 >> 0);
            var x1H = (byte)(x1 >> 8);
            var y1L = (byte)(y1 >> 0);
            var y1H = (byte)(y1 >> 8);
            var x2L = (byte)(x2 >> 0);
            var x2H = (byte)(x2 >> 8);
            var y2L = (byte)(y2 >> 0);
            var y2H = (byte)(y2 >> 8);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x64, 0x11, mode, pen, x1L, x1H, y1L, y1H, x2L, x2H, y2L, y2H });
        }

        public virtual void RealtimeBitImageDisplay(int x, int y, byte[] dn)
        {
            var xL = (byte)(x >> 0);
            var xH = (byte)(x >> 8);
            var yL = (byte)(y >> 0);
            var yH = (byte)(y >> 8);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x66, 0x11, xL, xH, yL, yH, 0x01 }.Concat(dn).ToArray());
        }

        public virtual void RamBitImageDefinition(int a, int s, byte[] dn)
        {
            var aL = (byte)(a >> 0);
            var aH = (byte)(a >> 8);
            var aE = (byte)(a >> 16);
            var sL = (byte)(s >> 0);
            var sH = (byte)(s >> 8);
            var sE = (byte)(s >> 16);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x66, 0x01, aL, aH, aE, sL, sH, sE }.Concat(dn).ToArray());
        }

        public virtual void FRomBitImageDefinition(int a, int s, byte[] dn)
        {
            var aL = (byte)(a >> 0);
            var aH = (byte)(a >> 8);
            var aE = (byte)(a >> 16);
            var sL = (byte)(s >> 0);
            var sH = (byte)(s >> 8);
            var sE = (byte)(s >> 16);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x10, aL, aH, aE, sL, sH, sE }.Concat(dn).ToArray());
        }

        public virtual void DownloadBitImageDisplay(byte m, int a, int yS, int x, int y)
        {
            var aL = (byte)(a >> 0);
            var aH = (byte)(a >> 8);
            var aE = (byte)(a >> 16);
            var ySL = (byte)(yS >> 0);
            var ySH = (byte)(yS >> 8);
            var xL = (byte)(x >> 0);
            var xH = (byte)(x >> 8);
            var yL = (byte)(y >> 0);
            var yH = (byte)(y >> 8);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x66, 0x10, m, aL, aH, aE, ySL, ySH, xL, xH, yL, yH, 0x01 });
        }

        public virtual void DownloadedBitImageScrollDisplay(byte m, int a, int yS, int x, int y, byte s)
        {
            var aL = (byte)(a >> 0);
            var aH = (byte)(a >> 8);
            var aE = (byte)(a >> 16);
            var ySL = (byte)(yS >> 0);
            var ySH = (byte)(yS >> 8);
            var xL = (byte)(x >> 0);
            var xH = (byte)(x >> 8);
            var yL = (byte)(y >> 0);
            var yH = (byte)(y >> 8);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x66, 0x90, m, aL, aH, aE, ySL, ySH, xL, xH, yL, yH, 0x01, s });
        }

        public virtual void HorizontalScrollDisplayQualitySelect(byte n)
        {
            WriteBytes(new byte[] { 0x1F, 0x6D, n });
        }

        public virtual void ReverseDisplay(byte n)
        {
            WriteBytes(new byte[] { 0x1F, 0x72, n });
        }

        public virtual void WriteMixtureDisplayMode(byte n)
        {
            WriteBytes(new byte[] { 0x1F, 0x77, n });
        }

        public virtual void WindowSelect(byte a)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x77, 0x01, a });
        }

        public virtual void UserWindowDefinitionOrCancel(byte a, byte b, int xP, int yP, int xS, int yS)
        {
            var xPL = (byte)(xP >> 0);
            var xPH = (byte)(xP >> 8);
            var yPL = (byte)(yP >> 0);
            var yPH = (byte)(yP >> 8);
            var xSL = (byte)(xS >> 0);
            var xSH = (byte)(xS >> 8);
            var ySL = (byte)(yS >> 0);
            var ySH = (byte)(yS >> 8);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x77, 0x02, a, b, xPL, xPH, yPL, yPH, xSL, xSH, ySL, ySH });
        }

        public virtual void DownloadCharacterOnOff(byte n)
        {
            WriteBytes(new byte[] { 0x1B, 0x25, n });
        }

        public virtual void DownloadCharacterDefinition(FontSize a, byte c1, byte c2, byte[] xn)
        {
            WriteBytes(new byte[] { 0x1B, 0x26, (byte)a, c1, c2 }.Concat(xn).ToArray());
        }

        public virtual void DownloadCharacterDelete(FontSize a, byte c)
        {
            WriteBytes(new byte[] { 0x1B, 0x3F, (byte)a, c });
        }

        public virtual void Font16x16DownloadedCharacterDefinition(byte c1, byte c2, byte[] xn)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x10, c1, c2 }.Concat(xn).ToArray());
        }

        public virtual void Font16x16DownloadedCharacterDelete(byte c1, byte c2)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x67, 0x11, c1, c2 });
        }

        public virtual void DownloadCharacterSave(DownloadCharacterSave a)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x11, (byte)a });
        }

        public virtual void DownloadCharacterRestore(DownloadCharacterSave a)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x21, (byte)a });
        }

        public virtual void FRomUserFontDefintion(FontSize m, byte[] p)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x13, (byte)m }.Concat(p).ToArray());
        }

        public virtual void UserSetUpModeStart(FontSize m, byte[] p)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x01, 0x49, 0x4E });
        }

        public virtual void UserSetUpModeEnd(FontSize m, byte[] p)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x02, 0x4F, 0x55, 0x54 });
        }

        public virtual void IoPortsInputOutputSetting(byte n, byte a)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x70, 0x01, n, a });
        }

        public virtual void IoPortOutput(byte n, byte a)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x70, 0x10, n, a });
        }

        public virtual void IoPortInput(byte n, byte a)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x70, 0x20, n });
        }

        public virtual void RamMacroDefineDelete(int p, byte[] dn)
        {
            var pL = (byte)(p >> 0);
            var pH = (byte)(p >> 8);

            WriteBytes(new byte[] { 0x1F, 0x3A, pL, pH }.Concat(dn).ToArray());
        }

        public virtual void FRomMacroDefineDelete(byte a, int p, byte t1, byte t2, byte[] dn)
        {
            var pL = (byte)(p >> 0);
            var pH = (byte)(p >> 8);

            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x12, a, pL, pH, t1, t2 }.Concat(dn).ToArray());
        }

        public virtual void MacroExecution(byte a, byte t1, byte t2, byte[] dn)
        {
            WriteBytes(new byte[] { 0x1F, 0x5E, a, t1, t2 }.Concat(dn).ToArray());
        }

        public virtual void MemorySwSetting(byte a, byte b)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x03, a, b });
        }

        public virtual void MemorySwDataSend(byte a)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x04, a });
        }

        public virtual void DisplayStatusSend(byte a, byte b, byte c)
        {
            WriteBytes(new byte[] { 0x1F, 0x28, 0x65, 0x40, a, b, c });
        }

        public virtual void MemoryRewriteMode()
        {
            WriteBytes(new byte[] { 0x1C, 0x7C, 0x4D, 0xD0, 0x4D, 0x4F, 0x44, 0x45, 0x49, 0x4E });
        }
    }

}