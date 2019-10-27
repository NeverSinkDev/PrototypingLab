namespace FuncCollection.Pendulum
{
    public class PendulumState<T,K> : IPendulumState
    {
        private Pendulum<T,K> pendulum;

        public PendulumState(Pendulum<T,K> pendulum)
        {
            this.pendulum = pendulum;
        }
    }

    public interface IPendulumState
    {
    }
}