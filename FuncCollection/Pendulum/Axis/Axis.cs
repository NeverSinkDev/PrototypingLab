using System;
using System.Collections.Generic;

namespace FuncCollection.Pendulum.Axis
{
    public interface IAxis<T>
    {
        T Value { get; set; }

        Dictionary<string, Func<T,bool>> AxisActions { get; set; }
    }
}
