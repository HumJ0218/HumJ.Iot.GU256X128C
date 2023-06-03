using HumJ.Iot.GU256X128C;
using System.Device.Gpio;
using System.Linq;

int[] d = new int[] { 27, 18, 17, 15, 14, 4, 3, 2 };
int wr = 22;
int rdy = 23;

GpioController gpio = new GpioController();
Initialize();

Console.WriteLine("ready");

var random = new Random();
var buffer = new byte[256 * 128 / 8];
while (true)
{
    var startTime = DateTime.Now;
    Anime();
    var endTime = DateTime.Now;

    var delay = endTime - startTime;
    Console.Title = $"{1000 / delay.TotalMilliseconds} FPS";
}

void Initialize()
{
    gpio.OpenPin(rdy, PinMode.Input);
    foreach (var p in d.Append(wr))
    {
        gpio.OpenPin(p, PinMode.Output, 0);
    }
}

void Anime()
{
    random.NextBytes(buffer);
    var bytes = GU_DMA.BitImageWrite(0, 0, buffer);
    foreach (var b in bytes)
    {
        SendByte(b);
    }
}

void SendByte(byte b)
{
    while (gpio.Read(rdy) == 0) ;
    gpio.Write(d.Select((p, i) => new PinValuePair(p, (b >> i) & 1)).Append(new PinValuePair(wr, 0)).ToArray());
    gpio.Write(wr, 1);
}