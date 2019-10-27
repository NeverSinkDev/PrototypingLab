using System.Collections.Generic;
using System.Linq;
using CommonModules.Attributes;
using FuncCollection.Mathem.MathOperations;
using FuncCollection.Mathem.NumericSources;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes.PEU
{
    // The prime factors of 13195 are 5, 7, 13 and 29.
    public class PE003 : AbstractModule
    {
        public override string Description => "003) Largest prime factor";

        [InputField]
        public double GoalNumber { get; set; } = 1000000;

        [OutputField]
        public double MaxPrimeOrFactor { get; set; } = 2;

        [OutputField]
        public double PrimesCount { get; set; } = 0;

        [ActionPerformer]
        public void CalculateLargestPrimeFactor()
        {
            List<double> results = new List<double>();
            double remainder = GoalNumber;

            // Process
            NumericSources.YieldNextPrime().TakeWhile(x => ProcessNumber(x) && x < remainder / 2).ToList();

            // Potentially recursively check a single number if it's a factor
            bool ProcessNumber(double i)
            {
                while (IsFactor(i))
                {
                    AdjustRemainder(i);
                    results.Add(i);

                    // Once the remainder is a prime, stop.
                    if (PrimeUtility.IsNumberPrime(remainder))
                    {
                        results.Add(remainder);
                        MaxPrimeOrFactor = remainder;
                        return false;
                    }
                }

                return true;
            }

            bool IsFactor (double i) => remainder % i == 0;
            void AdjustRemainder(double i) => remainder = remainder / i;
        }

        [ActionPerformer]
        public void CalculateLargestPrimeSLOW()
        {
            var done = this.HandleSmallNumbers();
            if (done)
            {
                return;
            }

            var primes = new List<FrequencyBeat>() { new FrequencyBeat(2) };

            for (double i = 3; i < GoalNumber; i++)
            {
                if (primes.Any(x => x.StepCheck(i)))
                {
                    continue;
                }

                primes.Add(new FrequencyBeat(i));
            }

            this.MaxPrimeOrFactor = primes.Last().GetFrequency();
            this.PrimesCount = 2 + primes.Count;
        }

        private bool HandleSmallNumbers()
        {
            if (GoalNumber <= 0)
            {
                this.MaxPrimeOrFactor = 0;
                this.PrimesCount = 0;
                return true;
            }

            if (GoalNumber == 1)
            {
                this.MaxPrimeOrFactor = 1;
                this.PrimesCount = 1;
                return true;
            }

            if (GoalNumber == 2)
            {
                this.MaxPrimeOrFactor = 2;
                this.PrimesCount = 2;
                return true;
            }

            return false;
        }
    }
}
