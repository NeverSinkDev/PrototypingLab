using System;
using System.Collections.Generic;
using FuncCollection.MethodPacking;

namespace FuncCollection.Pendulum
{
    public interface IPendulum<R>
    { 

        R Result { get; set; }

        IPackedFunc<R> GetExec();

        IPendulumRanking<R> Ranking { get; set; }

        IPendulumConstrains<R> Constrains { get; set; }

        IPendulumState State { get; set; }

        IPendulumModifications<R> Modifications { get; set; }

        List<Action> OnDoneActions { get; set; }
    }
}