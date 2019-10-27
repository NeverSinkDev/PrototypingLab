using FuncCollection.MethodPacking;

namespace FuncCollection.Pendulum
{
    public static class EPendulumCreation
    {
        public static Pendulum<R, PackedFunc<P1, P2, R>> ToPendulum<P1, P2, R>(this PackedFunc<P1, P2, R> method, P1 p1 = default(P1), P2 p2 = default(P2))
        {
            method.P1 = p1.Equals(default(P1)) ? method.P1 : p1;
            method.P2 = p2.Equals(default(P2)) ? method.P2 : p2;
            return new Pendulum<R, PackedFunc<P1, P2, R>>() { Me = method };
        }

        public static Pendulum<R, PackedFunc<P1, P2, P3, R>> ToPendulum<P1, P2, P3, R>(this PackedFunc<P1, P2, P3, R> method, P1 p1 = default(P1), P2 p2 = default(P2), P3 p3 = default(P3))
        {
            method.P1 = p1.Equals(default(P1)) ? method.P1 : p1;
            method.P2 = p2.Equals(default(P2)) ? method.P2 : p2;
            method.P3 = p3.Equals(default(P3)) ? method.P3 : p3;
            return new Pendulum<R, PackedFunc<P1, P2, P3, R>>() { Me = method };
        }
    }
}
