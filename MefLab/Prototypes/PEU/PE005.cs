using System;
using CommonModules.Attributes;
using FuncCollection.Collections;
using FuncCollection.Mathem.NumericSources;
using FuncCollection.Mathem.MathOperations;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;
using FuncCollection.CommonExtensions;

namespace MefLab.Prototypes.PEU
{
    // 2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
    // What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
    public class PE005 : AbstractModule
    {
        public override string Description => "005) Smallest multiple";

        [InputField]
        public double Input { get; set; } = 20;

        [OutputField]
        public double SmallestMultiple { get; set; } = 0;

        [ActionPerformer]
        public void CalculateSmallestMultiple()
        {
            var autod = new AutoDictionary<double>();
            double result = 1;

            // for the sequence from 1-to-Input, get prime factors for each and store new ones (or higher powers)
            // Then get all factors and multiply them

            // Back then I didn't know IEnumerable.Range o.o
            NumericSources.YieldSequenceFromTo(1, Input).ForEach(x => autod.RegisterIfNew(x.GetPrimeFactors()));
            autod.Dictionary.ForEach(x => result *= Math.Pow(x.Key, x.Value));

            this.SmallestMultiple = result;
        }
    }
}
