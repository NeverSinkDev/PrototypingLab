using System;
using FuncCollection.Pendulum.Axis;

namespace FuncCollection.Pendulum
{
    public static class EPendulumConfiguration
    {
        public static IPendulum<T> AddRanking<T>(this IPendulum<T> p, Func<T, float> eval, float goal = 1)
        {
            p.Ranking.AddEval(eval, goal);
            return p;
        }

        public static IPendulum<T> AddDimension<T>(this IPendulum<T> p, IAxis<T> axis)
        {
            p.Modifications.AddModificationDimension(axis);
            return p;
        }

        public static IPendulum<T> SetSteps<T>(this IPendulum<T> p, int i)
        {
            p.Ranking.SetMaxSteps(i);
            return p;
        }

        public static IPendulum<T> AddConstrain<T>(this IPendulum<T> p, Func<bool> constrain)
        {
            p.Constrains.AddConstrain(constrain);
            return p;
        }

        public static IPendulum<T> WhenDone<T>(this IPendulum<T> p, Action whenDoneAction)
        {
            p.OnDoneActions.Add(whenDoneAction);
            return p;
        }
    }
}
