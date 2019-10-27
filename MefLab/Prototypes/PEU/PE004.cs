using System;
using System.Linq;
using CommonModules.Attributes;
using FuncCollection.Mathem.NumericSources;
using FuncCollection.Mathem.MathOperations;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes.PEU
{
    public class PE004 : AbstractModule
    {
        // A palindromic number reads the same both ways.
        // The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.
        public override string Description => "004) Largest Palindrome Product";

        [InputField]
        public int TestNumber { get; set; } = 100001;

        [OutputField]
        public string Result { get; set; } = "";

        [ActionPerformer]
        public void DemoIsNumberPalindrome()
        {
            Result = TestNumber.IsPalindrome().ToString();
        }

        [ActionPerformer]
        public void GetLargestPalindrome()
        {
            Result = NumericSources.YieldPalindromesDescending(999)
                .SkipWhile(x => !this.IsCreatedWithTwoFactorsOfMaxLength(x,999))
                .First().ToString();
        }

        private bool IsCreatedWithTwoFactorsOfMaxLength(int product, int maxNum)
        {
            double sqrt = Math.Sqrt(product);

            for (int i = maxNum - 1; i >= 100; i--)
            {
                double res = (double)product / (double)i;

                if (res <= maxNum && Math.Round(res) == res && res >= 100)
                {
                    return true;
                }

                else if ( res >= sqrt)
                {
                    return false;
                }
            }

            return false;
        }
    }
}
