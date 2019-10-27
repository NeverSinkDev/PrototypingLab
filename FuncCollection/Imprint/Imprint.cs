using System;
using System.Collections.Generic;
using System.Reflection;
using FuncCollection.CommonExtensions;

namespace FuncCollection.Imprint
{
    /// <summary>
    /// Useful generic tool. Saves a state of an object and allows restoring to that state.
    /// Child class AutoImprint implements IDisposable interface for extra comfort
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Imprint<T> where T : class
    {
        public Imprint(T source)
        {
            this.Backup = source.AsDictionary();
            this.Value = source;
        }

        public IDictionary<string, object> Backup { get; private set; }
        public T Value { get; set; }

        public void Restore()
        {
            this.Value = this.ApplyValueFromDictionary(this.Value, this.Backup);
        }

        private T ApplyValueFromDictionary(T target, IDictionary<string, object> source)
        {
            foreach (var item in source)
            {
                target.GetType().GetTypeInfo().GetProperty(item.Key).SetValue(target, item.Value);
            }

            return target;
        }
    }

    public class AutoImprint<T> : Imprint<T>, IDisposable where T : class
    {
        private bool applyChanges = false;

        public AutoImprint(T source) : base(source)
        {

        }

        public void Dispose()
        {
            if (!applyChanges)
            {
                this.Restore();
            }
        }

        public void ApplyChanges()
        {
            this.applyChanges = true;
        }
    }

    public static class ImprintExtensions
    {
        public static Imprint<T> ToImprint<T>(this T value) where T : class
        {
            return new Imprint<T>(value);
        }

        public static AutoImprint<T> ToAutoImprint<T>(this T value) where T : class
        {
            return new AutoImprint<T>(value);
        }
    }
}
