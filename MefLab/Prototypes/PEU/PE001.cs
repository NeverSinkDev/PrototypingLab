using CommonModules.Attributes;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes.PEU
{
    public class PE001 : AbstractModule
    {
        public override string Description => "001) Multiples of 3 and 5";

        [InputField]
        public int Input { get; set; } = 999;

        [InputField]
        public int Mult1 { get; set; } = 3;

        [InputField]
        public int Mult2 { get; set; } = 5;

        [OutputField]
        public int Result { get; set; } = 0;

        [ActionPerformer]
        public void CalculateBrute()
        {
            var m1 = CountMultiplesOfUntil(Input, Mult1);
            var m2 = CountMultiplesOfUntil(Input, Mult2);
            var m3 = CountMultiplesOfUntil(Input, Mult2*Mult1);

            var multSum1 = GetMultSumBrute(m1, Mult1);
            var multSum2 = GetMultSumBrute(m2, Mult2);
            var multSum3 = GetMultSumBrute(m3, Mult2 * Mult1);

            this.Result = multSum1 + multSum2 - multSum3;
        }

        [ActionPerformer]
        public void Calculate()
        {
            var m1 = CountMultiplesOfUntil(Input, Mult1);
            var m2 = CountMultiplesOfUntil(Input, Mult2);
            var m3 = CountMultiplesOfUntil(Input, Mult2 * Mult1);

            var multSum1 = GetMultSum(m1, Mult1);
            var multSum2 = GetMultSum(m2, Mult2);
            var multSum3 = GetMultSum(m3, Mult2 * Mult1);

            this.Result = multSum1 + multSum2 - multSum3;
        }

        private int CountMultiplesOfUntil(int until, int of) => until / of;

        private int GetMultSum(int count, int of)
        {
            // Usage of "Triangular Numbers"
            return of * (count * (count + 1) / 2);
        }

        private int GetMultSumBrute(int count, int of)
        {
            var sum = 0;
            var step = 0;

            for (int i = 0; i < count; i++)
            {
                step += of;
                sum += step;
            }
            return sum;
        }
    }
}
