using CommonModules.Attributes;
using FuncCollection.Implementation.Writer;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes
{
    public class FunctionalWriter : AbstractModule
    {
        public override string Description => "Writer Monad Test";

        [InputField]
        public string A { get; set; } = "A";

        [InputField]
        public string B { get; set; } = "B";

        [OutputField]
        public string Output { get; set; } = "initial";

        [MultiLineTextField]
        public string Output2 { get; set; } = "initial";

        [ActionPerformer]
        public void ComputeLambda()
        {
            var writerResult = A.ToWriter().SelectMany(x => B.ToWriter(), (x, y) => x + y);
            Output = writerResult.Value;
            Output2 = string.Join("\r\n", writerResult.Log);
        }

        [ActionPerformer]
        public void ComputeLambdaAgain()
        {
            var writerResult = A.ToWriter("W1").SelectMany(x => B.ToWriter("W2"), (x, y) => x + y);
            var writerResult1 = writerResult.SelectMany(x => B.ToWriter("W2"), (x, y) => x + y);
            Output = writerResult1.Value;
            Output2 = string.Join("\r\n", writerResult1.Log);
        }
    }
}
