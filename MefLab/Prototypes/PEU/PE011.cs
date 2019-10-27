using System;
using System.Collections.Generic;
using System.Linq;
using CommonModules.Attributes;
using FuncCollection.Matrix.Matrix2D;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;
using FuncCollection.CommonExtensions;

namespace MefLab.Prototypes.PEU
{
    public class PE011 : AbstractModule
    {
        public override string Description => "011) Largest Product Grid Search";

        [InputField]
        public string InputGrid { get; set; } = "08 02 22 97 38 15 00 40 00 75 04 05 07 78 52 12 50 77 91 08\r\n49 49 99 40 17 81 18 57 60 87 17 40 98 43 69 48 04 56 62 00\r\n81 49 31 73 55 79 14 29 93 71 40 67 53 88 30 03 49 13 36 65\r\n52 70 95 23 04 60 11 42 69 24 68 56 01 32 56 71 37 02 36 91\r\n22 31 16 71 51 67 63 89 41 92 36 54 22 40 40 28 66 33 13 80\r\n24 47 32 60 99 03 45 02 44 75 33 53 78 36 84 20 35 17 12 50\r\n32 98 81 28 64 23 67 10 26 38 40 67 59 54 70 66 18 38 64 70\r\n67 26 20 68 02 62 12 20 95 63 94 39 63 08 40 91 66 49 94 21\r\n24 55 58 05 66 73 99 26 97 17 78 78 96 83 14 88 34 89 63 72\r\n21 36 23 09 75 00 76 44 20 45 35 14 00 61 33 97 34 31 33 95\r\n78 17 53 28 22 75 31 67 15 94 03 80 04 62 16 14 09 53 56 92\r\n16 39 05 42 96 35 31 47 55 58 88 24 00 17 54 24 36 29 85 57\r\n86 56 00 48 35 71 89 07 05 44 44 37 44 60 21 58 51 54 17 58\r\n19 80 81 68 05 94 47 69 28 73 92 13 86 52 17 77 04 89 55 40\r\n04 52 08 83 97 35 99 16 07 97 57 32 16 26 26 79 33 27 98 66\r\n88 36 68 87 57 62 20 72 03 46 33 67 46 55 12 32 63 93 53 69\r\n04 42 16 73 38 25 39 11 24 94 72 18 08 46 29 32 40 62 76 36\r\n20 69 36 41 72 30 23 88 34 62 99 69 82 67 59 85 74 04 36 16\r\n20 73 35 29 78 31 90 01 74 31 49 71 48 86 81 16 23 57 05 54\r\n01 70 54 71 83 51 54 69 16 92 33 48 61 43 52 01 89 19 67 48";

        [InputField] public int DigitChainLength { get; set; } = 4;

        [OutputField]
        public string OutputField { get; set; }

        public Matrix2D<int> Matrix = new Matrix2D<int>();

        [ActionPerformer]
        public void Calculate()
        {
            this.Matrix.Init(20,20);
            FillMatrix(InputGrid, Matrix);

            List<double> products = new List<double>();
            List<double> inBetween = new List<double>();

            this.Matrix.EnumConfig.ApplyPresetVertical();
            products.Add(CalculateMax());
            this.Matrix.EnumConfig.ApplyPresetHorizontal();
            products.Add(CalculateMax());
            this.Matrix.EnumConfig.ApplyPresetDiagonal1();
            products.Add(CalculateMax());

            // this part is potentially buggy! Needs extra testing
            this.Matrix.EnumConfig.ApplyPresetDiagonal2(19);
            products.Add(CalculateMax());

            this.OutputField = products.Max().ToString();

            double CalculateMax()
            {
                // The SaveTo part is just for debugging.
                return this.Matrix.ToRowEnum().Where(x => x.Count() > 4).Select(z => FindLargestChainProduct(z)).SaveTo(inBetween, false).Max();
            }

            void FillMatrix(string s, Matrix2D<int> m)
            {
                m.Fill(s.Replace("\r\n", " ").Split(' ').Select(x => Convert.ToInt32(x)).ToList());
            }
        }

        public double FindLargestChainProduct<T>(IEnumerable<Cell<T>> chain)
        {
            Queue<T> queue = new Queue<T>(DigitChainLength);

            return chain.Where(x => AddNumber(x.Value) == true).Select(x => ProductFromQueue()).Max();

            bool AddNumber(T n)
            {
                if (queue.Count == DigitChainLength)
                {
                    queue.Dequeue();
                }

                queue.Enqueue(n);
                if (queue.Count < DigitChainLength)
                {
                    return false;
                }

                return true;
            }

            double ProductFromQueue() => queue.Select(x => double.Parse(x.ToString())).Aggregate(1d, (x1, x2) => x1 * x2);
        }
    }
}
