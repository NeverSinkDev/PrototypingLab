using System.Linq;
using CommonModules.Attributes;
using FuncCollection.Mathem.NumericSources;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;
using FuncCollection.Mathem.MathOperations;
using FuncCollection.CommonExtensions;

namespace MefLab.Prototypes.PEU
{
    // https://www.math.upenn.edu/~deturck/m170/wk2/numdivisors.html
    public class PE012 : AbstractModule
    {
        public override string Description => "012) Highly divisible triangular number";

        [InputField]
        public int DivisorCount { get; set; } = 500;

        [InputField]
        public double OptionalInput { get; set; } = 144;

        [OutputField]
        public double Result { get; set; } = 0;

        [ActionPerformer]
        public void Calculate()
        {
            this.Result = NumericSources.YieldTriangleNumbers()
                .FirstOrDefault(x => ProbabilityUtility.GetAllCombinationsProducts(x.GetPrimeFactors().ProcessList(this.LogList)) > DivisorCount);

        }

        [ActionPerformer]
        public void DivisorCountOfANumber()
        {
            this.Result = ProbabilityUtility.GetAllCombinationsProducts(OptionalInput.GetPrimeFactors().ProcessList(this.LogList));
        }
    }
}
