using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using CommonModules.Attributes;
using FuncCollection.Mathem.MathOperations;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes.PEU
{
    public class PE014 : AbstractModule
    {
        public override string Description { get; } = "014) Collatz Longest Chain";

        public int MaxInitialNumberLength { get; set; } = 1000000;

        [ActionPerformer]
        public void GetSingleChain()
        {
            // Optimizations: Dont use linq
            // Use a simple If-max-system
            // Use a hashtable
            // At least use MaxBy by MoreLINQ
            this.Result = Enumerable.Range(1, MaxInitialNumberLength - 1).Select(x => (x, CollatzLength(x)))
                .Aggregate((i, j) => i.Item2 > j.Item2 ? i : j).Item1;
        }

        [OutputField]
        public int Result { get; set; }


        public int CollatzLength(int start)
        {
            long current = start;
            var length = 1;
            while (current > 1)
            {
                if (current.IsEven())
                {
                    current = current / 2;
                }
                else
                {
                    current = (current * 3) + 1;
                }

                length++;
            }

            return length;
        }
    }
}
