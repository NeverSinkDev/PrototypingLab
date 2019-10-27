using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using CommonModules.Attributes;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes.PEU
{
    public class PE017 : AbstractModule
    {
        public override string Description => "017) Number letter counts";

        [InputField] public double InputPower { get; set; } = 1000;

        [OutputField] public double SumOfDigits { get; set; } = 0;

        [ActionPerformer]
        public void Calculate()
        {
            for (int i = 1; i <= InputPower; i++)
            {
                var result = Analyze(i);
                LogList(result);

                this.SumOfDigits += result.Select(x => x.Trim().Length).Sum();

            }



        }

        public List<string> Analyze(int input)
        {
            List<string> result = new List<string>();
            List<int> splitInput = new List<int>();
            var indexedList = input.ToString().Select((x, i) => new {val = x, index = input.ToString().Length - i});

            bool tenCase = false;
            bool andNeeded = false;
            foreach (var item in indexedList)
            {
                switch (item.index)
                {
                    case 4:
                        result.Add(TranslateFirstLevel(item.val) + "thousand");
                        andNeeded = true;
                        break;
                    case 3:
                        result.Add(TranslateFirstLevel(item.val) != string.Empty ? TranslateFirstLevel(item.val) + "hundred" : string.Empty);
                        andNeeded = true;
                        break;
                    case 2:

                        if (andNeeded && item.val != '0')
                        {
                            result.Add("and");
                            andNeeded = false;
                        }

                        if (item.val == '1')
                        {
                            tenCase = true;
                        }
                        else
                        {
                            result.Add(TranslateSecondLevel(item.val));
                        }
                        break;
                    case 1:

                        if (andNeeded && item.val != '0')
                        {
                            result.Add("and");
                            andNeeded = false;
                        }

                    result.Add(tenCase ? TranslateSecondLevelTenCase(item.val) : TranslateFirstLevel(item.val));
                        break;
                }
            }

            return result;
        }

        public string TranslateSecondLevelTenCase(char s)
        {
            switch (s)
            {
                case '0':
                    return "ten";
                case '1':
                    return "eleven";
                case '2':
                    return "twelve";
                case '3':
                    return "thirteen";
                case '4':
                    return "fourteen";
                case '5':
                    return "fifteen";
                case '6':
                    return "sixteen";
                case '7':
                    return "seventeen";
                case '8':
                    return "eighteen";
                case '9':
                    return "nineteen";
            }

            return string.Empty;
        }

        public string TranslateSecondLevel(char s)
        {
            switch (s)
            {
                case '0':
                    return string.Empty;
                case '1':
                    return "ten";
                case '2':
                    return "twenty";
                case '3':
                    return "thirty";
                case '4':
                    return "forty";
                case '5':
                    return "fifty";
                case '6':
                    return "sixty";
                case '7':
                    return "seventy";
                case '8':
                    return "eighty";
                case '9':
                    return "ninety";
            }

            return string.Empty;
        }

        public string TranslateFirstLevel(char s)
        {
            switch (s)
            {
                case '0':
                    return string.Empty;
                case '1':
                    return "one";
                case '2':
                    return "two";
                case '3':
                    return "three";
                case '4':
                    return "four";
                case '5':
                    return "five";
                case '6':
                    return "six";
                case '7':
                    return "seven";
                case '8':
                    return "eight";
                case '9':
                    return "nine";
            }

            return string.Empty;
        }
    }
}
