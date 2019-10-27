﻿using System.Collections.Generic;
using System.Linq;
using CommonModules.Attributes;
using FuncCollection.Mathem.NumericSources;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes.PEU
{
    // Each new term in the Fibonacci sequence is generated by adding the previous two terms.
    // By starting with 1 and 2, the first 10 terms will be:
    // 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...
    public class PE002 : AbstractModule
    {
        public List<int> FiboSequence { get; set; } = new List<int>();

        public override string Description => "002) Even Fibonacci Numbers";

        [InputField]
        public int MaxNumber { get; set; } = 4000000;

        [OutputField]
        public long EvenFiboSum { get; set; } = 0;

        [ActionPerformer]
        public void GetEvenFiboSum()
        {
            EvenFiboSum = NumericSources.Fibonacci().TakeWhile(x => x < this.MaxNumber)
                                      .Where(x => x % 2L == 0L)
                                      .Sum();
        }
    }
}
