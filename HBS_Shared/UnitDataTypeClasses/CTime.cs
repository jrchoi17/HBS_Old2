using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS
{
    public class CTime
    {
        public enum Unit { s, min, hr, day }

        public double Value { get; set; }

        public Dictionary<Unit, double> Conversion { get; set; }
        public Dictionary<Unit, double> Offset { get; set; }

        public CTime(double value)
        {
            Initialization();
            Value = value;
        }

        public CTime(double value, Unit unit)
        {
            Initialization();
            Value = Conversion[unit] * value + Offset[unit];
        }

        public void Initialization()
        {
            Conversion = new Dictionary<Unit, double>();
            Offset = new Dictionary<Unit, double>();

            // s--> s
            Conversion.Add(Unit.s, 1.0);
            Offset.Add(Unit.s, 0.0);

            // s --> min
            Conversion.Add(Unit.min, 60.0);
            Offset.Add(Unit.min, 0.0);

            // s --> hr
            Conversion.Add(Unit.hr, 360.0);
            Offset.Add(Unit.hr, 0.0);

            // s --> day
            Conversion.Add(Unit.day, 86400.0);
            Offset.Add(Unit.day, 0.0);
        }

        public double this[Unit unit]
        {
            get
            {
                return (Value - Offset[unit]) / Conversion[unit];
            }
        }

        public static implicit operator CTime(double d)
        {
            return new CTime(d);
        }

        public static implicit operator double(CTime temperature)
        {
            return temperature.Value;
        }

        public static double operator +(CTime a, CTime b)
        {
            return a.Value + b.Value;
        }

        public static double operator -(CTime a, CTime b)
        {
            return a.Value - b.Value;
        }

        public static double operator *(CTime a, CTime b)
        {
            return a.Value * b.Value;
        }

        public static double operator /(CTime a, CTime b)
        {
            return a.Value / b.Value;
        }

        public static bool operator <(CTime a, CTime b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >(CTime a, CTime b)
        {
            return a.Value > b.Value;
        }

        public static bool operator ==(CTime a, CTime b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(CTime a, CTime b)
        {
            return a.Value != b.Value;
        }

        public static bool operator <=(CTime a, CTime b)
        {
            return a.Value <= b.Value;
        }

        public static bool operator >=(CTime a, CTime b)
        {
            return a.Value >= b.Value;
        }

        public override bool Equals(object a)
        {
            return !(a is CTime) ? false : this == (CTime)a;
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
