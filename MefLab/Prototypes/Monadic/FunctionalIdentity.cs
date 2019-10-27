using CommonModules.Attributes;
using FuncCollection.Implementation.Identity;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes
{
    public class FunctionalIdentity : AbstractModule
    {
        public override string Description => "Identity Monad Test";

        [InputField]
        public int A { get; set; } = 5;

        [InputField]
        public int B { get; set; } = 10;

        [OutputField]
        public string Output { get; set; } = "initial";

        [ActionPerformer]
        public void ComputeLambda()
        {
            Output = A.ToIdentity().SelectMany(x => B.ToIdentity(), (x, y) => x + y).Value.ToString();
        }
    }
}
