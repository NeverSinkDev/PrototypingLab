using System;
using System.Collections.Generic;
using FuncCollection.MethodPacking;

namespace FuncCollection.Pendulum
{
    public class Pendulum<R,M> : IPendulum<R>
    {
        public M Me { get; set; }

        public R Result { get; set; }

        public IPendulumRanking<R> Ranking { get; set; }

        public IPendulumConstrains<R> Constrains { get; set; }

        public IPendulumState State { get; set; }

        public IPendulumModifications<R> Modifications { get; set; }
        public List<Action> OnDoneActions { get; set; } = new List<Action>();

        public Pendulum()
        {
            this.Ranking = this.CreateRanking();
            this.Constrains = this.CreateConstrains();
            this.State = this.CreateState();
            this.Modifications = this.CreateModifications();
        }

        public IPackedFunc<R> GetExec()
        {
            return this.Me as IPackedFunc<R>;
        }

        public void Optimize()
        {

        }
    }
}
