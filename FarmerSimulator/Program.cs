using System;
using System.Threading;
using FarmerSimulator.Platform;

namespace FarmerSimulator
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var timer = new CountDownTimer.Builder()
                .AllTime(5000)
                .TickTime(1000)
                .OnStart(() => { Console.WriteLine("Start"); })
                .OnTick(() => { Console.WriteLine("Tick"); })
                .OnComplete(() => { Console.WriteLine("Complete"); })
                .Build();

            timer.Start();
            timer.Start();

            Thread.Sleep(6000);
            timer.Start();
        }

    }
}