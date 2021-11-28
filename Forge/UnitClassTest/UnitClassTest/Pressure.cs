using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitClassTest
{
    public class Pressure
    {
        public enum Unit { Pa, mmAq, bar, psi}

        public double Value { get; set; }

        public Dictionary<Unit, double> Conversion { get; set; }
        public Dictionary<Unit, double> Offset { get; set; }

        public Pressure(double value)
        {
            Initialization();
            Value = value;
        }

        public Pressure(double value, Unit unit)
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

        public static implicit operator Pressure(double d)
        {
            return new Pressure(d);
        }

        public static implicit operator double(Pressure temperature)
        {
            return temperature.Value;
        }

        public static double operator +(Pressure a, Pressure b)
        {
            return a.Value + b.Value;
        }

        public static double operator -(Pressure a, Pressure b)
        {
            return a.Value - b.Value;
        }

        public static double operator *(Pressure a, Pressure b)
        {
            return a.Value * b.Value;
        }

        public static double operator /(Pressure a, Pressure b)
        {
            return a.Value / b.Value;
        }

        public static bool operator <(Pressure a, Pressure b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >(Pressure a, Pressure b)
        {
            return a.Value > b.Value;
        }

        public static bool operator ==(Pressure a, Pressure b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(Pressure a, Pressure b)
        {
            return a.Value != b.Value;
        }

        public static bool operator <=(Pressure a, Pressure b)
        {
            return a.Value <= b.Value;
        }

        public static bool operator >=(Pressure a, Pressure b)
        {
            return a.Value >= b.Value;
        }

        public override bool Equals(object a)
        {
            return !(a is Pressure) ? false : this == (Pressure)a;
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
