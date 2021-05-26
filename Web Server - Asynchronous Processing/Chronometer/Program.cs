using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

using Chronometer.Models;

namespace Chronometer
{
    class Program
    {
        static void Main(string[] args)
        {
            var chronometer = new Chronometer.Models.Chronometer();

            while (true)
            {
                var line = Console.ReadLine().ToLower();

                switch (line)
                {
                    case "start": chronometer.Start(); break;
                    case "stop": chronometer.Stop(); break;
                    case "time": Console.WriteLine(chronometer.GetTime); break;
                    case "reset": chronometer.Reset(); break;
                    case "exit": return;

                    case "lap":
                        {
                            chronometer.Lap();
                            Console.WriteLine(chronometer.Laps.LastOrDefault());
                        }
                        break;

                    case "laps":
                        {
                            for (int i = 0; i < chronometer.Laps.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {chronometer.Laps[i]}");
                            }
                        }
                        break;                  
                }
            }
        }
    }
}
