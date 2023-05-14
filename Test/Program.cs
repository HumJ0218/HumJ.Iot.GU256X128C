using HumJ.Iot.GU256X128C;
using System.IO.Ports;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var serialPort = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One)
        {
            DtrEnable = true,
            RtsEnable = true,
        };
        serialPort.Open();


        var vfd = new GU_3900B(serialPort);
        vfd.DisplayClear();
        Thread.Sleep(100);

        var bytes = new byte[256 * 128 / 8];
        var random = new Random();

        while (true)
        {
            random.NextBytes(bytes);
            vfd.DotUnitRealtimeBitImageDisplay(0, 0, 256, 128, bytes);
        }
    }
}