using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitClassTest
{
    public class Length
    {
        public enum Unit { m, cm, mm, km }

        public double Value { get; set; }

        public Dictionary<Unit, double> Conversion { get; set; }
        public Dictionary<Unit, double> Offset { get; set; }

        public Length(double value)
        {
            Initialization();
            Value = value;
        }

        public Length(double value, Unit unit)
        {
            Initialization();
            Value = Conversion[unit] * value + Offset[unit];
        }

        public void Initialization()
        {
            Conversion = new Dictionary<Unit, double>();
            Offset = new Dictionary<Unit, double>();

            // m --> m
            Conversion.Add(Unit.m, 1.0);
            Offset.Add(Unit.m, 0.0);

            // m --> mm
            Conversion.Add(Unit.mm, 1.0 / 1000.0);
            Offset.Add(Unit.mm, 0.0);

            // m --> cm
            Conversion.Add(Unit.cm, 1.0 / 100.0);
            Offset.Add(Unit.cm, 0.0);

            // m --> km
            Conversion.Add(Unit.km, 1000.0);
            Offset.Add(Unit.km, 0.0);
        }

        public double this[Unit unit]
        {
            get
            {
                return (Value - Offset[unit]) / Conversion[unit];
            }
        }

        public static implicit operator Length(double d)
        {
            return new Length(d);
        }

        public static implicit operator double(Length temperature)
        {
            return temperature.Value;
        }

        public static double operator +(Length a, Length b)
        {
            return a.Value + b.Value;
        }

        public static double operator -(Length a, Length b)
        {
            return a.Value - b.Value;
        }

        public static double operator *(Length a, Length b)
        {
            return a.Value * b.Value;
        }

        public static double operator /(Length a, Length b)
        {
            return a.Value / b.Value;
        }

        public static bool operator <(Length a, Length b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >(Length a, Length b)
        {
            return a.Value > b.Value;
        }

        public static bool operator ==(Length a, Length b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(Length a, Length b)
        {
            return a.Value != b.Value;
        }

        public static bool operator <=(Length a, Length b)
        {
            return a.Value <= b.Value;
        }

        public static bool operator >=(Length a, Length b)
        {
            return a.Value >= b.Value;
        }

        public override bool Equals(object a)
        {
            return !(a is Length) ? false : this == (Length)a;
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
