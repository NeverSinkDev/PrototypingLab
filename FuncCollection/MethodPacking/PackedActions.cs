using System;

namespace FuncCollection.MethodPacking
{
    public interface IPackedAction
    {
        void Execute();
    }

    public class PackedAction : IPackedAction
    {
        public PackedAction(Action method)
        {
            this.Action = method;
        }

        public Action Action { get; set; }

        public void Execute()
        {
            this.Action();
        }
    }

    public class PackedAction<T>: IPackedAction
    {
        public PackedAction(Action<T> method, T p1)
        {
            this.Action = method;
            this.Param1 = p1;
        }

        public Action<T> Action { get; set; }
        public T Param1 { get; set; }

        public void Execute()
        {
            this.Action(Param1);
        }
    }

    public class PackedAction<T,K> : IPackedAction
    {
        public PackedAction(Action<T,K> method, T p1, K p2)
        {
            this.Action = method;
            this.Param1 = p1;
            this.Param2 = p2;
        }

        public Action<T,K> Action { get; set; }

        public T Param1 { get; set; }
        public K Param2 { get; set; }

        public void Execute()
        {
            this.Action(Param1, Param2);
        }
    }

    public class PackedAction<T, K, L> : IPackedAction
    {
        public PackedAction(Action<T, K, L> method, T p1, K p2, L p3)
        {
            this.Action = method;
            this.Param1 = p1;
            this.Param2 = p2;
            this.Param3 = p3;
        }

        public Action<T, K, L> Action { get; set; }

        public T Param1 { get; set; }
        public K Param2 { get; set; }
        public L Param3 { get; set; }

        public void Execute()
        {
            this.Action(Param1, Param2, Param3);
        }
    }

    public class PackedAction<T, K, L, M> : IPackedAction
    {
        public PackedAction(Action<T, K, L, M> method, T p1, K p2, L p3, M p4)
        {
            this.Action = method;
            this.Param1 = p1;
            this.Param2 = p2;
            this.Param3 = p3;
            this.Param4 = p4;

        }

        public Action<T, K, L, M> Action { get; set; }

        public T Param1 { get; set; }
        public K Param2 { get; set; }
        public L Param3 { get; set; }
        public M Param4 { get; set; }


        public void Execute()
        {
            this.Action(Param1, Param2, Param3, Param4);
        }
    }

    public class PackedAction<T, K, L, M, N> : IPackedAction
    {
        public PackedAction(Action<T, K, L, M, N> method, T p1, K p2, L p3, M p4, N p5)
        {
            this.Action = method;
            this.Param1 = p1;
            this.Param2 = p2;
            this.Param3 = p3;
            this.Param4 = p4;
            this.Param5 = p5;
        }

        public Action<T, K, L, M, N> Action { get; set; }

        public T Param1 { get; set; }
        public K Param2 { get; set; }
        public L Param3 { get; set; }
        public M Param4 { get; set; }
        public N Param5 { get; set; }

        public void Execute()
        {
            this.Action(Param1, Param2, Param3, Param4, Param5);
        }
    }

    public class PackedAction<T, K, L, M, N, O> : IPackedAction
    {
        public PackedAction(Action<T, K, L, M, N, O> method, T p1, K p2, L p3, M p4, N p5, O p6)
        {
            this.Action = method;
            this.Param1 = p1;
            this.Param2 = p2;
            this.Param3 = p3;
            this.Param4 = p4;
            this.Param5 = p5;
            this.Param6 = p6;
        }

        public Action<T, K, L, M, N, O> Action { get; set; }

        public T Param1 { get; set; }
        public K Param2 { get; set; }
        public L Param3 { get; set; }
        public M Param4 { get; set; }
        public N Param5 { get; set; }
        public O Param6 { get; set; }

        public void Execute()
        {
            this.Action(Param1, Param2, Param3, Param4, Param5, Param6);
        }
    }

    public static partial class CommandMaker
    {
        public static PackedAction PackMethod(Action method)
        {
            return new PackedAction(method);
        }

        public static PackedAction<T> PackMethod<T>(Action<T> method, T p1)
        {
            return new PackedAction<T>(method, p1);
        }

        public static PackedAction<T> PackMethod<T>(Action<T> method)
        {
            return new PackedAction<T>(method, default(T));
        }

        public static PackedAction<T, K> PackMethod<T,K>(Action<T,K> method, T p1, K p2)
        {
            return new PackedAction<T,K>(method, p1, p2);
        }

        public static PackedAction<T, K> PackMethod<T, K>(Action<T, K> method)
        {
            return new PackedAction<T, K>(method, default(T), default(K));
        }

        public static PackedAction<T, K, L> PackMethod<T, K, L>(Action<T, K, L> method, T p1, K p2, L p3)
        {
            return new PackedAction<T, K, L>(method, p1, p2, p3);
        }

        public static PackedAction<T, K, L> PackMethod<T, K, L>(Action<T, K, L> method)
        {
            return new PackedAction<T, K, L>(method, default(T), default(K), default(L));
        }

        public static PackedAction<T, K, L, M> PackMethod<T, K, L, M>(Action<T, K, L, M> method, T p1, K p2, L p3, M p4)
        {
            return new PackedAction<T, K, L, M>(method, p1, p2, p3, p4);
        }

        public static PackedAction<T, K, L, M> PackMethod<T, K, L, M>(Action<T, K, L, M> method)
        {
            return new PackedAction<T, K, L, M>(method, default(T), default(K), default(L), default(M));
        }

        public static PackedAction<T, K, L, M, N> PackMethod<T, K, L, M, N>(Action<T, K, L, M, N> method, T p1, K p2, L p3, M p4, N p5)
        {
            return new PackedAction<T, K, L, M, N>(method, p1, p2, p3, p4, p5);
        }

        public static PackedAction<T, K, L, M, N> PackMethod<T, K, L, M, N>(Action<T, K, L, M, N> method)
        {
            return new PackedAction<T, K, L, M, N>(method, default(T), default(K), default(L), default(M), default(N));
        }

        public static PackedAction<T, K, L, M, N, O> PackMethod<T, K, L, M, N, O>(Action<T, K, L, M, N, O> method, T p1, K p2, L p3, M p4, N p5, O p6)
        {
            return new PackedAction<T, K, L, M, N, O>(method, p1, p2, p3, p4, p5, p6);
        }

        public static PackedAction<T, K, L, M, N, O> PackMethod<T, K, L, M, N, O>(Action<T, K, L, M, N, O> method)
        {
            return new PackedAction<T, K, L, M, N, O>(method, default(T), default(K), default(L), default(M), default(N), default(O));
        }
    }
}
