using System;
using System.Linq;
using CommonModules.Attributes;
using FuncCollection.Mathem.NumericSources;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes.PEU
{
    // Difference between the sum of the squares and the square of the sum.
    public class PE006 : AbstractModule
    {
        public override string Description => "006) Sum of Squares";

        [OutputField]
        public double SumOfSquares1 { get; set; } = 0;

        [OutputField]
        public double SquareOfSums2 { get; set; } = 0;

        [OutputField]
        public double DifferenceResult { get; set; } = 0;

        [InputField]
        public double InputCount { get; set; } = 100;

        [ActionPerformer]
        public void Calculate()
        {
            this.SumOfSquares1 = NumericSources.YieldSequenceFromTo(1, InputCount).Select(x => x * x).Sum();
            this.SquareOfSums2 = Math.Pow(NumericSources.YieldSequenceFromTo(1, InputCount).Sum(), 2);
            DifferenceResult = SquareOfSums2 - SumOfSquares1;
        }
    }
}
