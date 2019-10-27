using System;

namespace FuncCollection.MethodPacking
{
    public interface IPackedFunc<R>
    {
        R Execute();

        R Result { get; set; }
    }

    public class PackedFunc<R> : IPackedFunc<R>
    {
        public PackedFunc(Func<R> method)
        {
            this.Action = method;
        }

        public R Result { get; set; } = default(R);
        Func<R> Action { get; set; }

        R IPackedFunc<R>.Execute()
        {
            return this.Action();
        }
    }

    public class PackedFunc<T,R> : IPackedFunc<R>
    {
        public R Result { get; set; } = default(R);
        public PackedFunc(Func<T,R> method, T p1)
        {
            this.Action = method;
            this.P1 = p1;
        }

        Func<T,R> Action { get; set; }
        public T P1 { get; set; }

        public R Execute()
        {
            return this.Action(P1);
        }
    }

    public class PackedFunc<T,K, R> : IPackedFunc<R>
    {
        public R Result { get; set; } = default(R);
        public PackedFunc(Func<T,K, R> method, T p1, K p2)
        {
            this.Action = method;
            this.P1 = p1;
            this.P2 = p2;

        }

        Func<T, K, R> Action { get; set; }
        public T P1 { get; set; }
        public K P2 { get; set; }


        public R Execute()
        {
            return this.Action(P1, P2);
        }
    }

    public class PackedFunc<T, K, L, R> : IPackedFunc<R>
    {
        public R Result { get; set; } = default(R);
        public PackedFunc(Func<T, K, L, R> method, T p1, K p2 , L p3)
        {
            this.Action = method;
            this.P1 = p1;
            this.P2 = p2;
            this.P3 = p3;

        }

        Func<T, K, L, R> Action { get; set; }
        public T P1 { get; set; }
        public K P2 { get; set; }
        public L P3 { get; set; }


        public R Execute()
        {
            return this.Action(P1, P2, P3);
        }
    }

    public class PackedFunc<T, K, L, M, R> : IPackedFunc<R>
    {
        public R Result { get; set; } = default(R);
        public PackedFunc(Func<T, K, L, M, R> method, T p1, K p2, L p3, M p4)
        {
            this.Action = method;
            this.P1 = p1;
            this.P2 = p2;
            this.P3 = p3;
            this.P4 = p4;
        }

        Func<T, K, L, M, R> Action { get; set; }
        public T P1 { get; set; }
        public K P2 { get; set; }
        public L P3 { get; set; }
        public M P4 { get; set; }

        public R Execute()
        {
            return this.Action(P1, P2, P3, P4);
        }
    }

    public class PackedFunc<T, K, L, M, N, R> : IPackedFunc<R>
    {
        public R Result { get; set; } = default(R);
        public PackedFunc(Func<T, K, L, M, N, R> method, T p1, K p2, L p3, M p4, N p5)
        {
            this.Action = method;
            this.P1 = p1;
            this.P2 = p2;
            this.P3 = p3;
            this.P4 = p4;
            this.P5 = p5;

        }

        Func<T, K, L, M, N, R> Action { get; set; }
        public T P1 { get; set; }
        public K P2 { get; set; }
        public L P3 { get; set; }
        public M P4 { get; set; }
        public N P5 { get; set; }


        public R Execute()
        {
            return this.Action(P1, P2, P3, P4, P5);
        }
    }

    public class PackedFunc<T, K, L, M, N, O, R> : IPackedFunc<R>
    {
        public R Result { get; set; } = default(R);
        public PackedFunc(Func<T, K, L, M, N, O, R> method, T p1, K p2, L p3, M p4, N p5, O p6)
        {
            this.Action = method;
            this.P1 = p1;
            this.P2 = p2;
            this.P3 = p3;
            this.P4 = p4;
            this.P5 = p5;
            this.P6 = p6;
        }

        Func<T, K, L, M, N, O, R> Action { get; set; }
        public T P1 { get; set; }
        public K P2 { get; set; }
        public L P3 { get; set; }
        public M P4 { get; set; }
        public N P5 { get; set; }
        public O P6 { get; set; }

        public R Execute()
        {
            return this.Action(P1, P2, P3, P4, P5, P6);
        }
    }

    public static partial class CommandMaker
    {
        public static PackedFunc<R> PackMethod<R>(Func<R> method)
        {
            return new PackedFunc<R>(method);
        }

        public static PackedFunc<T, R> PackMethod< T, R>(Func<T,R> method, T p1)
        {
            return new PackedFunc<T,R>(method, p1);
        }

        public static PackedFunc<T, R> PackMethod< T, R>(Func<T,R> method)
        {
            return new PackedFunc<T,R>(method, default(T));
        }

        public static PackedFunc<T, K, R> PackMethod< T, K, R>(Func<T, K,R> method, T p1, K p2)
        {
            return new PackedFunc<T, K,R>(method, p1, p2);
        }

        public static PackedFunc<T, K, R> PackMethod< T, K, R>(Func<T, K,R> method)
        {
            return new PackedFunc<T, K,R>(method, default(T), default(K));
        }

        public static PackedFunc<T, K, L, R> PackMethod< T, K, L,R>(Func<T, K, L,R> method, T p1, K p2, L p3)
        {
            return new PackedFunc<T, K, L,R>(method, p1, p2, p3);
        }

        public static PackedFunc<T, K, L, R> PackMethod< T, K, L, R>(Func<T, K, L,R> method)
        {
            return new PackedFunc<T, K, L,R>(method, default(T), default(K), default(L));
        }

        public static PackedFunc<T, K, L, M, R> PackMethod< T, K, L, M, R>(Func<T, K, L, M,R> method, T p1, K p2, L p3, M p4)
        {
            return new PackedFunc<T, K, L, M,R>(method, p1, p2, p3, p4);
        }

        public static PackedFunc<T, K, L, M, R> PackMethod< T, K, L, M, R>(Func<T, K, L, M,R> method)
        {
            return new PackedFunc<T, K, L, M,R>(method, default(T), default(K), default(L), default(M));
        }

        public static PackedFunc<T, K, L, M, N, R> PackMethod< T, K, L, M, N, R>(Func<T, K, L, M, N,R> method, T p1, K p2, L p3, M p4, N p5)
        {
            return new PackedFunc<T, K, L, M, N,R>(method, p1, p2, p3, p4, p5);
        }

        public static PackedFunc<T, K, L, M, N, R> PackMethod< T, K, L, M, N, R>(Func<T, K, L, M, N,R> method)
        {
            return new PackedFunc<T, K, L, M, N,R>(method, default(T), default(K), default(L), default(M), default(N));
        }

        public static PackedFunc<T, K, L, M, N, O, R> PackMethod< T, K, L, M, N, O, R>(Func<T, K, L, M, N, O,R> method, T p1, K p2, L p3, M p4, N p5, O p6)
        {
            return new PackedFunc<T, K, L, M, N, O,R>(method, p1, p2, p3, p4, p5, p6);
        }

        public static PackedFunc<T, K, L, M, N, O, R> PackMethod< T, K, L, M, N, O, R>(Func<T, K, L, M, N, O,R> method)
        {
            return new PackedFunc<T, K, L, M, N, O,R>(method, default(T), default(K), default(L), default(M), default(N), default(O));
        }
    }

}



