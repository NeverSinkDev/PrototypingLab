using CommonModules.Attributes;
using FuncCollection.Collections.ICompositionCollection;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MefLab.Prototypes.PEU
{
    public class PE018 : AbstractModule
    {
        public override string Description => "018) Triangle Path Problem";

        [InputField]
        public string InputText { get; set; } =
@"75
95 64
17 47 82
18 35 87 10
20 04 82 47 65
19 01 23 75 03 34
88 02 77 73 07 63 67
99 65 04 28 06 16 70 92
41 41 26 56 83 40 80 70 33
41 48 72 33 47 32 37 16 94 29
53 71 44 65 25 43 91 52 97 51 14
70 11 33 28 77 73 17 78 39 68 17 57
91 71 52 38 17 14 91 43 58 50 27 29 48
63 66 04 68 89 53 67 30 73 16 69 87 40 31
04 62 98 27 23 09 70 98 73 93 38 53 60 04 23";

        [OutputField] public double MaximumResult { get; set; } = 0;

        [ActionPerformer] public void Calculate()
        {
            // First we need to transform the string into something workable -> a dicitonary of lines and values.
            Dictionary<int, List<int>> lineNumberDictionary = CreateDictionaryRepresentation();

            // Now we transform that representation into a composition.
            ComposableInt root = new ComposableInt(75).ToCompositionRoot();
            var lastLine = new List<ComposableInt>() { root };

            foreach (var line in lineNumberDictionary)
            {
                if (line.Key == 0)
                {
                    continue;
                }

                for (int i = 0; i <= line.Key; i++)
                {
                    var node = new ComposableInt(line.Value[i]);

                    if (i != line.Key)
                    {
                        lastLine[i].LinkChild(node);
                    }

                    if (i != 0)
                    {
                        lastLine[i - 1].LinkChild(node);
                    }
                }

                // get all children from current line, but filter repeats out.
                lastLine = lastLine.SelectMany(x => x.Composition.Children).Distinct().ToList();
            }

            // Now we go back up, while simplifying every line. There's a best choice at each fork.
            var parents = lastLine.SelectMany(x => x.GoToParents()).Distinct().ToList();

            while (parents.Count > 1)
            {
                parents.ForEach(x => x.CurrentValue += x.Composition.Children.Select(z => z.CurrentValue).Max());
                parents = parents.SelectMany(x => x.GoToParents()).Distinct().ToList();
            }

            MaximumResult = parents.Select(x => x.CurrentValue += x.Composition.Children.Select(z => z.CurrentValue).Max()).Single();
        }

        private Dictionary<int, List<int>> CreateDictionaryRepresentation()
        {
            return InputText.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None)
                .Select((x, c) =>
                {
                    var numbers = x.Split(' ').Select(z => int.Parse(z)).ToList();
                    var line = new
                    {
                        key = c,
                        value = numbers
                    };
                    return line;
                })
                .ToDictionary(x => x.key, x => x.value);
        }
    }

    public class ComposableInt : IComposable<ComposableInt>
    {
        public ComposableInt(int value)
        {
            this.CurrentValue = value;
        }

        public ICompositionElement<ComposableInt> Composition { get; set; } = new CompositionElement<ComposableInt>();
        public int CurrentValue { get; set; } = 0;
    }
}
