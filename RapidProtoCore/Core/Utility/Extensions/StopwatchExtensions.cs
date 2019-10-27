using System;
using System.Diagnostics;
using System.Threading.Tasks;
using RapidProtoCore.Core.Architecture.Modularity;

namespace RapidProtoCore.Core.Utility.Extensions
{
    public static class StopwatchExtensions
    {
        public static void Time(this Stopwatch sw, Action action, object iterations, AbstractModule writeTo)
        {
            var iterCount = (int)iterations;

            writeTo.ClearMessage();
            sw.Reset();
            sw.Start();

            for (int i = 0; i < iterCount; i++)
            {
                action();
            }
            sw.Stop();

            writeTo.Timing = sw.ElapsedMilliseconds.ToString();
        }

        public static async Task TimeAsync(this Stopwatch sw, Func<Task> action, object iterations, AbstractModule writeTo)
        {
            var iterCount = (int)iterations;

            writeTo.ClearMessage();
            sw.Reset();
            sw.Start();

            for (int i = 0; i < iterCount; i++)
            {
                await action();
            }

            sw.Stop();

            writeTo.Timing = sw.ElapsedMilliseconds.ToString();
        }
    }
}
