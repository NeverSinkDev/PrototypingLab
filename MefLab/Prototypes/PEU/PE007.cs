using System.Linq;
using CommonModules.Attributes;
using FuncCollection.Mathem.NumericSources;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes.PEU
{
    public class PE007 : AbstractModule
    {
        public override string Description => "007) Prime Numbers";

        [InputField]
        public double NumberOfPrime { get; set; } = 10001;

        [OutputField]
        public double ThePrimeNumberItself { get; set; } = 0;

        [ActionPerformer]
        public void Calculate()
        {
            this.ThePrimeNumberItself = NumericSources.YieldNextPrime().Skip((int)NumberOfPrime - 1).First();
        }
    }
}
