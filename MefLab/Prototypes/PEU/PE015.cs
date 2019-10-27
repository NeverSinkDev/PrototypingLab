using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonModules.Attributes;
using FuncCollection.Mathem.MathOperations;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes.PEU
{
    public class PE015 : AbstractModule
    {
        public override string Description { get; } = "015) Lattice Paths.";

        [InputField] public int GridX { get; set; } = 20;

        [InputField] public int GridY { get; set; } = 20;

        [OutputField] public double TotalComb { get; set; } = 0;

        [ActionPerformer]
        public void CalculatePathCount()
        {
            // The length is always the same (X+Y). It always consists of 20 movements left and 20 right
            // The number of pathes is the number of combinations of 10 selections on a pool of 20 spots.
            TotalComb = ProbabilityUtility.GetCombinations(GridX + GridY, GridX);
        }    
    }
}
