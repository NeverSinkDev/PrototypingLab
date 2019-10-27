using System.Collections.Generic;

namespace FuncCollection.Collections.ICompositionCollection
{
    public class CompositionElement<T> : ICompositionElement<T> where T : class, IComposable<T>
    {
        public T Value { get; set; }
        public ICollection<T> Children { get; set; } = new List<T>();
        public ICollection<T> Parent { get; set; } = new List<T>();
        public int ID { get; set; }
        public CompositionRegistry<T> Registry { get; set; }
    }
}
