using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuncCollection.Mathem.MathOperations
{
    public static class ProbabilityUtility
    {
        public static double Factorial(int input)
        {
            double result = 1;
            while (input > 0)
            {
                result*= input;
                input--;
            }

            return result;
        }

        public static double GetCombinations(int nTotal, int rTake)
        {
            return (Factorial(nTotal) / (Factorial(nTotal - rTake) * Factorial(rTake)));
        }

        public static double GetPermutations(int nTotal, int rTake)
        {
            return (Factorial(nTotal) / (Factorial(nTotal - rTake)));
        }

        public static double GetAllCombinations(int nTotal)
        {
            double result = 0;
            for (int i = 1; i <= nTotal; i++)
            {
                result += GetCombinations(nTotal, i);
            }

            return result;
        }

        public static double GetAllCombinationsProducts(IEnumerable<double> enumeration)
        {
            var distincts = enumeration.Where(x => x > 1).Distinct();
            return distincts.Select(x => enumeration.Where(z => z == x).Count() + 1).Aggregate(1, (a, b) => a *= b);
        }
    }
}
