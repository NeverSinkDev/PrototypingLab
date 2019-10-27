using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FuncCollection.MethodPacking;

namespace FuncCollection.Pendulum.Axis
{
    public class NumericAxis : IAxis<double>
    {
        public double Value { get; set; }

        public Accessor<double> Accessor { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public Dictionary<string, Func<double,bool>> AxisActions { get; set; } = new Dictionary<string, Func<double, bool>>();
    }

    public static class NumAxisTools
    {
        public static NumericAxis CreateAxis(Expression<Func<double>> expr, double minValue = 0, double maxValue = double.MaxValue)
        {
            var accessor = new Accessor<double>(expr);
            var axis = new NumericAxis() { Value = accessor.Get(), MinValue = minValue, MaxValue = maxValue };

            axis.Accessor = accessor;
            axis.AddActions();
            return axis;
        }



        public static void AddActions(this NumericAxis me)
        {
            me.AxisActions.Add("Inc", x =>
            {
                me.Value++;
                if (me.Value > me.MaxValue)
                {
                    me.Value = me.MaxValue;
                    return false;
                }

                me.Accessor.Set(me.Value);
                return true;
            });

            me.AxisActions.Add("Sub", x =>
            {
                me.Value--;
                if (me.Value < me.MinValue)
                {
                    me.Value = me.MinValue;
                    return false;
                }

                me.Accessor.Set(me.Value);
                return true;
            });
        }
    }
}
