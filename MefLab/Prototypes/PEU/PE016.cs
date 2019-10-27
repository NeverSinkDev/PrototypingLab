using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using CommonModules.Attributes;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes.PEU
{
    public class PE016 : AbstractModule
    {
        public override string Description { get; } = "016) Power Digit Sum";

        [InputField] public double InputPower { get; set; } = 1000;

        [OutputField] public double SumOfDigits { get; set; } = 0;

        [ActionPerformer]
        public void Calculate()
        {
            List<int> start = new List<int>(){2};

            for (int i = 1; i < InputPower; i++)
            {
                start = Double(start);
                LogList(start);

            }

            this.SumOfDigits = start.Sum();

        }

        public List<int> Double(List<int> input)
        {
            List<int> result = new List<int>();

            bool overlap = false;
            for (int i = 0; i <= input.Count - 1; i++)
            {
                var multResult = input[i]*2;

                if (overlap)
                {
                    multResult++;
                    overlap = false;
                }

                if (multResult >= 10)
                {
                    multResult -= 10;
                    overlap = true;
                }

                result.Add(multResult);
            }

            if (overlap)
            {
                result.Add(1);
            }


            return result;
        }
    }
}
