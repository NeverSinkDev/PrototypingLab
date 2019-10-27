using System;
using System.Collections.Generic;
using System.Linq;

namespace FuncCollection.Mathem.MathOperations
{
    public static class PrimeUtility
    {
        public static bool IsNumberPrime(double i)
        {
            var max = System.Math.Sqrt(i);
            if (System.Math.Round(max) == max || max <= 1)
            { return false; }

            var success = NumericSources.NumericSources.YieldNextPrime().SkipWhile(x => x < max && i % x != 0).FirstOrDefault();

            if (success < max)
            {
                return false;
            }

            return true;
        }

        public static IEnumerable<double> GetPrimeFactors(this double input)
        {
            List<double> results = new List<double>() { 1 };
            var initialValue = input;

            NumericSources.NumericSources.YieldNextPrime().TakeWhile(x => ProcessNumber(x) && x < input / 2).ToList();

            if (results.Count == 1)
            {
                results.Add(input);
            }

            return results;

            // Potentially recursively check a single number if it's a factor
            bool ProcessNumber(double i)
            {
                while (IsFactor(i))
                {
                    Adjustinput(i);
                    results.Add(i);

                    // Once the input is a prime, stop.
                    if (PrimeUtility.IsNumberPrime(input))
                    {
                        results.Add(input);
                        return false;
                    }
                }

                return true;
            }

            bool IsFactor(double i) => input % i == 0;
            void Adjustinput(double i) => input = input / i;
        }
    }
}
