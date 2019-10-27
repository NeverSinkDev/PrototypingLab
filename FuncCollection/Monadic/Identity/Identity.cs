using System;

namespace FuncCollection.Implementation.Identity
{
    /// <summary>
    /// Most primitive possible monad. Encapsulates a single value and provides the required utility functions to execute binds 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Identity<T>
    {
        public T Value { get; private set; }
        public Identity(T value) { this.Value = value; }
    }

    public static class IdentityExtensions
    {
        public static Identity<T> ToIdentity<T>(this T value)
        {
            return new Identity<T>(value);
        }

        public static Identity<V> SelectMany<T, U, V>(this Identity<T> id, Func<T, Identity<U>> k, Func<T, U, V> s)
        {
            return s(id.Value, k(id.Value).Value).ToIdentity();
        }
    }
}
