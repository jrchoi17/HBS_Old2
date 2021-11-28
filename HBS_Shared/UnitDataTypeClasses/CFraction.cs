using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS
{
    public class CFraction
    {
        public enum Unit { num, percent, permille }

        public double Value { get; set; }

        public Dictionary<Unit, double> Conversion { get; set; }
        public Dictionary<Unit, double> Offset { get; set; }

        public CFraction(double value)
        {
            Initialization();
            Value = value;
        }

        public CFraction(double value, Unit unit)
        {
            Initialization();
            Value = Conversion[unit] * value + Offset[unit];
        }

        public void Initialization()
        {
            Conversion = new Dictionary<Unit, double>();
            Offset = new Dictionary<Unit, double>();

            // num --> num
            Conversion.Add(Unit.num, 1.0 / 100);
            Offset.Add(Unit.num, 0.0);

            // num --> permille 
            Conversion.Add(Unit.permille, 1.0 / 1000.0);
            Offset.Add(Unit.permille, 0.0);

            // num --> percent
            Conversion.Add(Unit.percent, 1.0 / 100.0);
            Offset.Add(Unit.percent, 0.0);
        }

        public double this[Unit unit]
        {
            get
            {
                return (Value - Offset[unit]) / Conversion[unit];
            }
        }

        public static implicit operator CFraction(double d)
        {
            return new CFraction(d);
        }

        public static implicit operator double(CFraction temperature)
        {
            return temperature.Value;
        }

        public static double operator +(CFraction a, CFraction b)
        {
            return a.Value + b.Value;
        }

        public static double operator -(CFraction a, CFraction b)
        {
            return a.Value - b.Value;
        }

        public static double operator *(CFraction a, CFraction b)
        {
            return a.Value * b.Value;
        }

        public static double operator /(CFraction a, CFraction b)
        {
            return a.Value / b.Value;
        }

        public static bool operator <(CFraction a, CFraction b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >(CFraction a, CFraction b)
        {
            return a.Value > b.Value;
        }

        public static bool operator ==(CFraction a, CFraction b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(CFraction a, CFraction b)
        {
            return a.Value != b.Value;
        }

        public static bool operator <=(CFraction a, CFraction b)
        {
            return a.Value <= b.Value;
        }

        public static bool operator >=(CFraction a, CFraction b)
        {
            return a.Value >= b.Value;
        }

        public override bool Equals(object a)
        {
            return !(a is CFraction) ? false : this == (CFraction)a;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
