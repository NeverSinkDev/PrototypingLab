using System;
using System.Collections.Generic;
using System.Linq;
using FuncCollection.Pendulum.Axis;

namespace FuncCollection.Pendulum
{
    public class PendulumModification<T,K> : IPendulumModifications<T>
    {
        private Pendulum<T,K> pendulum;

        public List<IAxis<T>> Modifications { get; set; } = new List<IAxis<T>>();

        public List<KeyValuePair<string, Func<T, bool>>> AvailableActions { get; set; } =
            new List<KeyValuePair<string, Func<T, bool>>>();

        public PendulumModification(Pendulum<T, K> pendulum) => this.pendulum = pendulum;

        public void AddModificationDimension(IAxis<T> modification) => this.Modifications.Add(modification);

        public void MapModifications() => this.AvailableActions = this.Modifications.SelectMany(x => x.AxisActions).ToList();

        public bool PerformModification(int num) => this.AvailableActions[num].Value(default(T));

        public int GetModificationCount() => this.AvailableActions.Count;

    }

    public interface IPendulumModifications<T>
    {
        void AddModificationDimension(IAxis<T> modification);

        bool PerformModification(int num);

        int GetModificationCount();

        void MapModifications();
    }
}
