using System;
using System.Collections.Generic;

namespace FuncCollection.Implementation.Writer
{
    public class Writer<T>
    {
        public Writer(T value, string name = "unnamed")
        {
            this.Value = value;
            this.Name = name;
            this.Log.Add($"Writer \"{name}\" created with initial value {value.ToString()}");
        }

        public string Name { get; set; }

        public List<string> Log { get; set; } = new List<string>();

        public T Value { get; set; }
    }

    public static class WriterExtensions
    {
        public static Writer<T> ToWriter<T>(this T value, string name = "unnamed")
        {
            return new Writer<T>(value, name);
        }

        public static Writer<V> SelectMany<T, U, V>(this Writer<T> id, Func<T, Writer<U>> k, Func<T, U, V> s)
        {
            
            var writerResult = k(id.Value);

            writerResult.Log.Add($"SelectMany applied on value { id.Value } and { writerResult.Value }");

            var result = s(id.Value, writerResult.Value).ToWriter();

            result.Log.InsertRange(0, writerResult.Log);
            result.Log.InsertRange(0,id.Log);

            return result;
        }
    }
}
