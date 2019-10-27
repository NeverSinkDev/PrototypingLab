using System.Windows;
using CommonModules.Attributes;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes
{
    /// <summary>
    /// PE001
    /// </summary>
    public class NotifyDemo : AbstractModule
    {
        public override string Description { get => "Modularity Prototype"; }

        [InputField]
        public int a { get; set; } =  1;

        [InputField]
        public int b { get; set; } = 2;

        [InputField]
        public int n { get; set; } = 3;

        [InputField]
        public int d { get; set; } = 5;

        [InputField]
        public string configValue { get; set; } = "Test";

        [OutputField]
        public string result { get; set; } = "initial result";

        [ActionPerformer]
        public void TestSumFunction()
        {
            result = (a + b + n + d).ToString();
        }

        [ActionPerformer]
        public void TestOutputData()
        {
            MessageBox.Show("A: " + a + " // B: " + b + " // N: "+ n + " // Result: " + result);
        }

        [ActionPerformer]
        public void IncrementAll()
        {
            a++;
            b++;
            n++;
            d++;
        }
    }
}
