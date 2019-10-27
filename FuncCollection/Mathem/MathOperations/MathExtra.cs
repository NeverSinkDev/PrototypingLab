using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FuncCollection.Mathem.MathOperations
{
    public static class MathExtra
    {
        public static float Distance(float a, float b)
        {
            return Math.Abs(Math.Max(a, b) - Math.Min(a, b));
        }

        public static double Distance(double a, double b)
        {
            return Math.Abs(Math.Max(a, b) - Math.Min(a, b));
        }

        public static int Distance(int a, int b)
        {
            return Math.Abs(Math.Max(a, b) - Math.Min(a, b));
        }

        public static bool IsEven(this int a)
        {
            return a % 2 == 0;
        }
        public static bool IsEven(this long a)
        {
            return a % 2 == 0;
        }
        public static bool IsEven(this double a)
        {
            return a % 2 == 0;
        }
    }
}
