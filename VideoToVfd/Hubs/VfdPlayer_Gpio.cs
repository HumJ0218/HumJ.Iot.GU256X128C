using System.Device.Gpio;
using System.Diagnostics;
using HumJ.Iot.GU256X128C;

namespace VideoToVfd.Hubs
{
    static class VfdPlayer_Gpio
    {
        public static byte[] bytes = new byte[256 * 128 / 8];

        static readonly int[] d = new int[] { 27, 18, 17, 15, 14, 4, 3, 2 };
        static int wr = 22;
        static int rdy = 23;
        static GpioController gpio = new GpioController();

        public static void Start()
        {
            Initialize();

            Console.WriteLine("ready");

            Task.Run(() =>
            {
                try
                {
                    var sw1 = new Stopwatch();

                    while (true)
                    {
                        sw1.Restart();

                        foreach (var b in bytes)
                        {
                            WriteParallel(b);
                        }

                        var delay = sw1.Elapsed.TotalMilliseconds;
                        Console.Title = $"{1000 / delay} FPS";
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
            });
        }

        private static void WriteParallel(byte b)
        {
            gpio.Write(wr, 0);
            for (var i = 0; i < 8; i++)
            {
                gpio.Write(d[i], (b >> i) & 1);
            }

            while (gpio.Read(rdy) == 0) ;
            gpio.Write(wr, 1);
        }

        static void Initialize()
        {
            bytes = GU_DMA.BitImageWrite(0, 0, bytes);

            gpio.OpenPin(rdy, PinMode.InputPullUp);
            foreach (var p in d.Append(wr))
            {
                gpio.OpenPin(p, PinMode.Output, 0);
            }
        }
    }
}