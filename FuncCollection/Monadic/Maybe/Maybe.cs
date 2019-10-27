using System;

namespace FuncCollection.Implementation.Maybe
{
    /// <summary>
    /// Monad - encapsulates null-possible operations and combines them into chains. Returns null and cancels the pipeline if one of the
    /// Bind functions results in a null
    /// </summary>
    /// <typeparam name="T">Type of data</typeparam>
    public class Maybe<T>
    {
        public readonly static Maybe<T> Nothing = new Maybe<T>();

        public T Value { get; private set; }

        public bool HasValue { get; private set; }

        public Maybe()
        {
            HasValue = false;
        }

        public Maybe(T value)
        {
            if (value == null)
            {
                HasValue = false;
            }
            else
            {
                HasValue = true;
            }

            Value = value;
            
        }
    }

    public static class MaybeExtensions
    {
        public static Maybe<T> ToMaybe<T>(this T value)
        {
            if (value == null || value.Equals(default(T)))
            {
                return new Maybe<T>();
            }

            return new Maybe<T>(value);
        }

        public static Maybe<V> SelectMany<T, U, V>(this Maybe<T> id, Func<T, Maybe<U>> k, Func<T, U, V> s)
        {
            if (!id.HasValue)
            {
                return Maybe<V>.Nothing;
            }

            var k1 = k(id.Value);

            if (!k1.HasValue)
            {
                return Maybe<V>.Nothing;
            }

            var result = s(id.Value, k1.Value).ToMaybe();

            return result;
        }
    }
}
