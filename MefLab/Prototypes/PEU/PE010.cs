using System.Linq;
using CommonModules.Attributes;
using FuncCollection.Mathem.NumericSources;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes.PEU
{
    public class PE010 : AbstractModule
    {
        // 142913828922 -> solved, but way too slow.
        public override string Description => "010) PrimeSum";

        [InputField]
        public double MaxNumber { get; set; } = 2000000;

        [OutputField]
        public double Result { get; set; }

        [ActionPerformer]
        public void CalculateSLOW()
        {
            this.Result = NumericSources.YieldNextPrime().TakeWhile(x => x < MaxNumber).Sum();
        }
    }
}
