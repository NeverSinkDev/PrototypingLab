using CommonModules.Attributes;
using FuncCollection.Implementation.Maybe;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes
{
    public class FunctionalMaybe : AbstractModule
    {
        public override string Description => "Maybe Monad Test";

        [InputField]
        public string A { get; set; } = "A";

        [InputField]
        public string B { get; set; } = "B";

        public string C { get; set; } = null;

        [OutputField]
        public string Output { get; set; }

        [ActionPerformer]
        public void AselectMaybeB()
        {
            Output = A.ToMaybe().SelectMany(x => B.ToMaybe(), (x, y) => x + y).Value;
        }

        [ActionPerformer]
        public void BselectMaybeA()
        {   
            Output = B.ToMaybe().SelectMany(x => A.ToMaybe(), (x, y) => x + y).Value;
        }

        [ActionPerformer]
        public void AselectMaybeNull()
        {
            var O = A.ToMaybe().SelectMany(x => C.ToMaybe(), (x, y) => x + y);
            Output = O.HasValue ? O.Value : "Nothing";
        }

        [ActionPerformer]
        public void NullSelectMaybeA()
        {
            var O  = C.ToMaybe().SelectMany(x => A.ToMaybe(), (x, y) => x + y);
            Output = O.HasValue ? O.Value : "Nothing";   
        }
    }
}
