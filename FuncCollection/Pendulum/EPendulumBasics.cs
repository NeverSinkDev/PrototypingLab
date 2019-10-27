using System;
using System.Collections.Generic;

namespace FuncCollection.Pendulum
{
    public static class EPendulumBasics
    {
        /// <summary>
        /// The "projection method". Perform modifications on a specialized object and unit a common interface.
        /// </summary>
        public static IPendulum<R> Define<R, T>(this Pendulum<R, T> p, Func<Pendulum<R, T>, IPendulum<R>> ex) => ex(p);

        public static B Lift<B, T>(this B me, Func<B, T> liftOp) where T : class, B where B : class => liftOp(me);

        public static B Extract<B, T>(this T me) where T : class, B where B : class => me;

        public static IPendulumRanking<R> CreateRanking<R, K>(this Pendulum<R, K> pendulum) => new PendulumRanking<R, K>(pendulum);

        public static IPendulumConstrains<R> CreateConstrains<R, K>(this Pendulum<R, K> pendulum) => new PendulumConstrains<R, K>(pendulum);

        public static IPendulumModifications<R> CreateModifications<R, K>(this Pendulum<R, K> pendulum) => new PendulumModification<R, K>(pendulum);

        public static IPendulumState CreateState<R, K>(this Pendulum<R, K> pendulum) => new PendulumState<R, K>(pendulum);

        public static R Execute<R>(this IPendulum<R> p)
        {
            p.Modifications.MapModifications();

            var optimized = p.Ranking.IsOptimized();
            var constrain = p.Constrains.TestConstrains();

            var rng = new Random();
            var mods = p.Modifications.GetModificationCount();
            var modNum = 0;

            p.Ranking.Reset();

            while (p.Ranking.HasStepsLeft())
            {
                // Bruteforce-like Strategy
                p.Modifications.PerformModification(modNum);

                p.GetExec().Result = p.GetExec().Execute();
                p.Ranking.Evaluate();

                optimized = p.Ranking.IsOptimized();
                constrain = p.Constrains.TestConstrains();

                if (optimized && constrain)
                {
                    break;
                }

                if (!constrain)
                {
                    modNum = rng.Next(0, mods);
                }
            }

            p.OnDoneActions.ForEach(x => x());

            return default(R);
        }

        public static IEnumerable<R> Start<R>(this IPendulum<R> p)
        {
            while (!p.Ranking.IsOptimized())
            {
                yield return p.GetExec().Execute();
            }
        }
    }
}
