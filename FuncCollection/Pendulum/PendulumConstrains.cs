using System;
using System.Collections.Generic;
using System.Linq;

namespace FuncCollection.Pendulum
{
    public class PendulumConstrains<T,K> : IPendulumConstrains<T>
    {
        private Pendulum<T,K> pendulum;

        public List<Func<bool>> Constrains { get; set; } = new List<Func<bool>>();

        public PendulumConstrains(Pendulum<T,K> pendulum)
        {
            this.pendulum = pendulum;
        }

        public void AddConstrain(Func<bool> constrain)
        {
            this.Constrains.Add(constrain);
        }

        public bool TestConstrains()
        {
            return this.Constrains.All(x => x() == true);
        }
    }

    public interface IPendulumConstrains<T>
    {
        void AddConstrain(Func<bool> constrain);

        bool TestConstrains();
    }
}