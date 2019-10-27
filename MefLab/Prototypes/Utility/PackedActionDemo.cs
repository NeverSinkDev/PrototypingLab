using CommonModules.Attributes;
using FuncCollection.MethodPacking;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes
{
    class PackedActionDemo : AbstractModule
    {
        public override string Description => "Method Packing Demo";

        public IPackedAction PackedAction { get; set; }

        public IPackedFunc<int> PackedAction1 { get; set; }

        [InputField]
        public int Input { get; set; } = 2;

        [OutputField]
        public int Output { get; set; } = 0;

        [ActionPerformer]
        public void ExecutePackAndRun()
        {
            this.PackedAction = CommandMaker.PackMethod(this.TestMethod, this.Input);
            this.PackedAction.Execute();
        }

        [ActionPerformer]
        public void ExecutePack()
        {
            this.PackedAction = CommandMaker.PackMethod(this.TestMethod, this.Input);
        }

        [ActionPerformer]
        public void ExecutePack2()
        {
            this.PackedAction = CommandMaker.PackMethod(this.TestMethod2, this.Input, string.Empty);
        }

        [ActionPerformer]
        public void ExecutePack3()
        {
            var packed = CommandMaker.PackMethod(this.TestMethod3, this.Input, string.Empty);
        }

        [ActionPerformer]
        public void ExecuteRunPacked()
        {
            this.PackedAction.Execute();
        }
                   
        [ActionPerformer]
        public void ExecuteNormal()
        {
            this.TestMethod(this.Input);
        }

        public void TestMethod(int i)
        {
            //i++;
            //this.Output += this.Input;
        }

        public void TestMethod2(int i, string j)
        {
            //i++;
            //this.Output += this.Input;
        }

        public int TestMethod3(int i, string j)
        {
            return 5;
        }
    }
}
