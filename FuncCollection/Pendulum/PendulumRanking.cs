using System;
using System.Collections.Generic;
using System.Linq;
using FuncCollection.Mathem.MathOperations;

namespace FuncCollection.Pendulum
{
    public class PendulumRanking<T,K> : IPendulumRanking<T>
    {
        private Pendulum<T,K> pendulum;
        private float lastRes = 0;
        private float goal = 1;
        private float maxSteps = 10000;
        public float Steps { get; set; } = 0;

        private List<Tuple<float, Func<T, float>>> EvaluationList { get; set; } =
            new List<Tuple<float, Func<T, float>>>();

        public PendulumRanking(Pendulum<T,K> pendulum)
        {
            this.pendulum = pendulum;
        }

        public bool IsOptimized()
        {
            if (this.EvaluationList.Count == 0)
            {
                return true;
            }

            return this.lastRes == this.goal;
        }

        public void Reset()
        {
            this.Steps = 0;
        }

        public bool HasStepsLeft()
        {
            return Steps <= maxSteps;
        }

        public void Evaluate()
        {
            if (this.EvaluationList.Count == 0)
            {
                return;
            }

            this.lastRes = this.EvaluationList.Average(x => MathExtra.Distance(x.Item2(pendulum.Result), x.Item1));
            Steps++;
        }

        public void SetMaxSteps(int i)
        {
            this.maxSteps = i;
        }

        public void AddEval(Func<T, float> func, float goalNum)
        {
            this.EvaluationList.Add(new Tuple<float, Func<T, float>>(goalNum, func));
        }
    }

    public interface IPendulumRanking<T>
    {
        float Steps { get; set; }

        void Reset();

        bool IsOptimized();

        bool HasStepsLeft();

        void Evaluate();

        void SetMaxSteps(int i);

        void AddEval(Func<T, float> func, float goalNum);
    }
}
