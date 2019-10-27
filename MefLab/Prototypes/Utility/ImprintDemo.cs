using System.Diagnostics;
using CommonModules.Attributes;
using FuncCollection.Imprint;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes
{
    public class ImprintDemo : AbstractModule
    {
        public ExampleObject ExampleObj { get; set; } = new ExampleObject();

        public Imprint<ExampleObject> ExampleImprint { get; set; }

        public override string Description => "Disposable Imprint Pattern";

        [InputField]
        public int ChangeTo { get; set; } = 10;

        [OutputField]
        public int Output1 { get; set; } = 0;

        [OutputField]
        public int Output2 { get; set; } = 0;

        [ActionPerformer]
        public void CreateImprint()
        {
            ExampleObj = new ExampleObject();
            ExampleImprint = new Imprint<ExampleObject>(this.ExampleObj);
            this.Output1 = ExampleImprint.Value.P1;
            this.Output2 = ExampleObj.P1;
        }

        [ActionPerformer]
        public void ChangeImprint()
        {
            ExampleImprint.Value.P1 = ChangeTo;
            this.Output1 = ExampleImprint.Value.P1;
            this.Output2 = ExampleObj.P1;
        }

        [ActionPerformer]
        public void RestoreImprint()
        {
            ExampleImprint.Restore();
            this.Output1 = ExampleImprint.Value.P1;
            this.Output2 = ExampleObj.P1;
        }

        [ActionPerformer]
        public void AutoImprint()
        {
            var x = new ExampleObject();
            Debug.WriteLine(x.P1);

            using (var imp = x.ToAutoImprint())
            {
                x.P1 = 10;
                Debug.WriteLine(x.P1);
            }

            Debug.WriteLine(x.P1);

        }
    }

    public class ExampleObject
    {
        public int P1 { get; set; } = -999;
    }
}
