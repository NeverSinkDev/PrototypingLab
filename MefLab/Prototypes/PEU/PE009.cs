using System;
using CommonModules.Attributes;
using FuncCollection.CommonExtensions;
using FuncCollection.MethodPacking;
using FuncCollection.Pendulum;
using FuncCollection.Pendulum.Axis;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace MefLab.Prototypes.PEU
{
    public class PE009 : AbstractModule
    {
        public override string Description => "009) Pyta Triangle 1000";

        [InputField] public double ExpectedSum { get; set; } = 1000;

        [OutputField] public double A { get; set; } = 1;

        [OutputField] public double B { get; set; } = 1;

        [OutputField] public double C { get; set; } = 1;

        [OutputField] public double UsedSteps { get; set; } = 0;

        [ActionPerformer]
        public void Calculate()
        {
            A = 1;
            B = 1;
            C = 1;
            UsedSteps = 0;

            var pendulum = CommandMaker.PackMethod(this.SumSides, A, B, C)
                .ToPendulum()
                .Define(x => x
                    .SetSteps(10000000)
                    .AddConstrain(() => x.Me.Result == ExpectedSum)
                    .AddConstrain(() => this.Pytha(x.Me.P1, x.Me.P2, x.Me.P3))
                    .AddConstrain(() => x.Me.P2 > x.Me.P1 && x.Me.P3 > x.Me.P2)
                    .AddDimension(NumAxisTools.CreateAxis(() => x.Me.P1, 0, 600))
                    .AddDimension(NumAxisTools.CreateAxis(() => x.Me.P2, 0, 600))
                    .AddDimension(NumAxisTools.CreateAxis(() => x.Me.P3, 0, 600))
                    .WhenDone(() =>
                    {
                        A = x.Me.P1;
                        B = x.Me.P2;
                        C = x.Me.P3;
                        UsedSteps = x.Ranking.Steps;
                    }));

            pendulum.Execute();
        }

        public double SumSides(double a, double b, double c) => a + b + c;

        public bool Pytha(double a, double b, double c) => Math.Pow(a, 2) + Math.Pow(b, 2) == Math.Pow(c, 2);
    }
}
