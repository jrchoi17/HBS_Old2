using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS
{
    public class CPressure
    {
        public enum Unit { Pa, mmAq, bar, psi }

        public double Value { get; set; }

        public Dictionary<Unit, double> Conversion { get; set; }
        public Dictionary<Unit, double> Offset { get; set; }

        public CPressure(double value)
        {
            Initialization();
            Value = value;
        }

        public CPressure(double value, Unit unit)
        {
            Initialization();
            Value = Conversion[unit] * value + Offset[unit];
        }

        public void Initialization()
        {
            Conversion = new Dictionary<Unit, double>();
            Offset = new Dictionary<Unit, double>();

            // Pa--> Pa
            Conversion.Add(Unit.Pa, 1.0);
            Offset.Add(Unit.Pa, 0.0);

            // Pa --> mmAq
            Conversion.Add(Unit.mmAq, 9.80665);
            Offset.Add(Unit.mmAq, 0.0);

            // Pa --> bar
            Conversion.Add(Unit.bar, 100000);
            Offset.Add(Unit.bar, 0.0);

            // Pa --> psi
            Conversion.Add(Unit.psi, 6894.757);
            Offset.Add(Unit.psi, 0.0);
        }

        public double this[Unit unit]
        {
            get
            {
                return (Value - Offset[unit]) / Conversion[unit];
            }
        }

        public static implicit operator CPressure(double d)
        {
            return new CPressure(d);
        }

        public static implicit operator double(CPressure temperature)
        {
            return temperature.Value;
        }

        public static double operator +(CPressure a, CPressure b)
        {
            return a.Value + b.Value;
        }

        public static double operator -(CPressure a, CPressure b)
        {
            return a.Value - b.Value;
        }

        public static double operator *(CPressure a, CPressure b)
        {
            return a.Value * b.Value;
        }

        public static double operator /(CPressure a, CPressure b)
        {
            return a.Value / b.Value;
        }

        public static bool operator <(CPressure a, CPressure b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >(CPressure a, CPressure b)
        {
            return a.Value > b.Value;
        }

        public static bool operator ==(CPressure a, CPressure b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(CPressure a, CPressure b)
        {
            return a.Value != b.Value;
        }

        public static bool operator <=(CPressure a, CPressure b)
        {
            return a.Value <= b.Value;
        }

        public static bool operator >=(CPressure a, CPressure b)
        {
            return a.Value >= b.Value;
        }

        public override bool Equals(object a)
        {
            return !(a is CPressure) ? false : this == (CPressure)a;
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
