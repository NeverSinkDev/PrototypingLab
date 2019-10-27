using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncCollection.Collections
{
    public class AutoDictionary<T>
    {
        public Dictionary<T, int> Dictionary { get; set; } = new Dictionary<T, int>();

        public int this[T val]
        {
            get
            {
                if (this.Dictionary.ContainsKey(val))
                {
                    return this.Dictionary[val];
                }

                return 0;
            }

            set
            {
                if (!this.Dictionary.ContainsKey(val))
                {
                    this.Dictionary.Add(val, 0);
                }

                this.Dictionary[val] = value;
            }
        }

        public void Register(T val)
        {
            this[val]++;
        }

        public void RegisterIfNew(IEnumerable<T> val)
        {
            var localAutoDic = new AutoDictionary<T>();
            foreach (var v in val)
            {
                localAutoDic.Register(v);
            }

            foreach (var pair in localAutoDic.Dictionary)
            {
                if (this[pair.Key] < pair.Value)
                {
                    this[pair.Key] = pair.Value;
                }
            }
        }
    }
}
