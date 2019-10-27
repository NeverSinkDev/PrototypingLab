using System.Collections.Generic;
using System.Linq;

namespace FuncCollection.Mathem.NumericSources
{
    public static class NumericSources
    {
        public static IEnumerable<double> YieldNextPrime()
        {
            var primes = new List<FrequencyBeat>() { new FrequencyBeat(2) };
            double i = 2;
            yield return i;

            do
            {
                if (primes.Any(x => x.StepCheck(i)))
                {
                    continue;
                }

                primes.Add(new FrequencyBeat(i));
                yield return i;
            }
            while (++i < double.MaxValue - 1);
        }

        public static IEnumerable<double> YieldTriangleNumbers()
        {
            double sum = 0;
            double i = 0;
            while (true)
            {
                yield return sum += ++i;
            }
        }

        public static IEnumerable<double> YieldSequenceFromTo(double from, double to)
        {
            if (from <= to)
            {
                for (double i = from; i <= to; i++)
                {
                    yield return i;
                }
            }
            else
            {
                for (double i = to; i >= from; i--)
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<int> YieldSequenceFromTo(int from, int to)
        {
            if (from <= to)
            {
                for (int i = from; i <= to; i++)
                {
                    yield return i;
                }
            }
            else
            {
                for (int i = to; i >= from; i--)
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<int> YieldPalindromesDescending(int maxNum)
        {
            for (int i = maxNum - 1; i >= 0; i--)
            {
                yield return BuildPalindrome(i);
            }

            int BuildPalindrome(int z) => int.Parse(z.ToString() + string.Concat(z.ToString().Reverse()));
        }

        public static IEnumerable<long> Fibonacci()
        {
            long current = 0;
            long next = 1;
            while (true)
            {
                yield return current;
                long temp = next;
                next = current + next;
                current = temp;
            }
        }
    }
}
